using Fluxera.Enumeration.EntityFrameworkCore;
using Fluxera.Repository.EntityFrameworkCore;
using Guardians.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Guardians.Infrastructure.Contexts;

public sealed class SceneConfiguration : IEntityTypeConfiguration<Scene>
{
    private readonly Action<EntityTypeBuilder<Scene>>? _callback;

    public SceneConfiguration(Action<EntityTypeBuilder<Scene>>? callback = null)
    {
        _callback = callback;
    }

    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Scene> builder)
    {
        builder.Property(s => s.ID).ValueGeneratedOnAdd();
        builder.Property(s => s.Title).HasMaxLength(100);
        builder.UseRepositoryDefaults();
        builder.UseEnumeration(true);
        _callback?.Invoke(builder);
    }
}