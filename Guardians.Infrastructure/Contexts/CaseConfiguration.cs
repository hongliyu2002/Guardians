using Fluxera.Repository.EntityFrameworkCore;
using Guardians.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Guardians.Infrastructure.Contexts;

public sealed class CaseConfiguration : IEntityTypeConfiguration<Case>
{
    private readonly Action<EntityTypeBuilder<Case>>? _callback;

    public CaseConfiguration(Action<EntityTypeBuilder<Case>>? callback = null)
    {
        _callback = callback;
    }

    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Case> builder)
    {
        builder.Property(c => c.ID).ValueGeneratedOnAdd();
        builder.HasOne(c => c.Scene).WithMany().HasForeignKey(c => c.SceneID).OnDelete(DeleteBehavior.Restrict);
        builder.Property(c => c.Description).HasMaxLength(500);
        builder.Property(c => c.Address).HasMaxLength(200);
        builder.Property(c => c.PhotoUrl).HasMaxLength(500).IsUnicode(false);
        builder.Property(c => c.ReporterNo).HasMaxLength(50).IsUnicode(false);
        builder.Property(c => c.ReporterName).HasMaxLength(50);
        builder.Property(c => c.ReporterMobile).HasMaxLength(20).IsUnicode(false);
        builder.Property(c => c.CreatedBy).HasMaxLength(50);
        builder.Property(c => c.LastModifiedBy).HasMaxLength(50);
        builder.Property(c => c.DeletedBy).HasMaxLength(50);
        builder.UseRepositoryDefaults();
        _callback?.Invoke(builder);
    }
}