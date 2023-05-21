using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Guardians.Infrastructure.Contexts;

public class GuardiansDbContextDesignFactory : IDesignTimeDbContextFactory<GuardiansDbContext>
{
    /// <inheritdoc />
    public GuardiansDbContext CreateDbContext(string[] args)
    {
        var connectionString = "Server=localhost;Integrated Security=False;User Id=sa;Password=Bosshong2010;TrustServerCertificate=True;Database=GuardiansDB";
        var optionsBuilder = new DbContextOptionsBuilder<GuardiansDbContext>().UseSqlServer(connectionString);
        return new GuardiansDbContext(optionsBuilder.Options);
    }
}