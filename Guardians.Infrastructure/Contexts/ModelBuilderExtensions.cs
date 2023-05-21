using Guardians.Domain;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Guardians.Infrastructure.Contexts;

[PublicAPI]
public static class ModelBuilderExtensions
{
    public static void AddCaseEntity(this ModelBuilder modelBuilder, Action<EntityTypeBuilder<Case>>? callback = null)
    {
        modelBuilder.ApplyConfiguration(new CaseConfiguration(callback));
    }

    public static void AddSceneEntity(this ModelBuilder modelBuilder, Action<EntityTypeBuilder<Scene>>? callback = null)
    {
        modelBuilder.ApplyConfiguration(new SceneConfiguration(callback));
    }
}