using Fluxera.Repository.EntityFrameworkCore;
using JetBrains.Annotations;

namespace Guardians.Infrastructure.Contexts;

[PublicAPI]
internal sealed class GuardiansRepositoryContext : EntityFrameworkCoreContext
{
    /// <inheritdoc />
    protected override void ConfigureOptions(EntityFrameworkCoreContextOptions options)
    {
        options.UseDbContext<GuardiansDbContext>();
    }
}