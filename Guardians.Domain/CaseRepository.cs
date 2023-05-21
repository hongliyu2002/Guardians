using Fluxera.Repository;
using Guardians.Domain.Shared;
using JetBrains.Annotations;

namespace Guardians.Domain;

[PublicAPI]
public interface ICaseRepository : IRepository<Case, CaseId>
{
}

[UsedImplicitly]
internal sealed class CaseRepository : Repository<Case, CaseId>, ICaseRepository
{
    /// <inheritdoc />
    public CaseRepository(IRepository<Case, CaseId> innerRepository)
        : base(innerRepository)
    {
    }
}