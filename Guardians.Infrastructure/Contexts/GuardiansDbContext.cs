using Fluxera.Extensions.Hosting.Modules.Persistence;
using Fluxera.Repository;
using Fluxera.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Guardians.Infrastructure.Contexts;

public sealed class GuardiansDbContext : DbContext
{
    private readonly IDatabaseConnectionStringProvider? _dbConnectionStringProvider;
    private readonly IDatabaseNameProvider? _dbNameProvider;

    /// <inheritdoc />
    public GuardiansDbContext()
    {
    }

    public GuardiansDbContext(DbContextOptions<GuardiansDbContext> options, IDatabaseConnectionStringProvider? dbConnectionStringProvider = null, IDatabaseNameProvider? dbNameProvider = null)
        : base(options)
    {
        _dbConnectionStringProvider = dbConnectionStringProvider;
        _dbNameProvider = dbNameProvider;
    }

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }
        var repositoryName = new RepositoryName("Default");
        var dbName = _dbNameProvider?.GetDatabaseName(repositoryName);
        var dbConnectionString = _dbConnectionStringProvider?.GetConnectionString(repositoryName);
        dbConnectionString ??= "Server=localhost;Integrated Security=True;TrustServerCertificate=True;";
        dbConnectionString = dbConnectionString.EnsureEndsWith(";");
        dbConnectionString += $"Database={dbName ?? "GuardiansDB"}";
        // optionsBuilder.EnableDetailedErrors();
        // optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseSqlServer(dbConnectionString, builder =>
                                                        {
                                                            builder.EnableRetryOnFailure(3);
                                                            builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                                                        });
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddCaseEntity(builder => builder.ToTable("Cases"));
        modelBuilder.AddSceneEntity(builder => builder.ToTable("Scenes"));
    }
}