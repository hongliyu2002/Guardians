using Fluxera.StronglyTypedId;
using JetBrains.Annotations;

namespace Guardians.Domain.Shared;

[PublicAPI]
public sealed class CaseId : StronglyTypedId<CaseId, Guid>
{
    public CaseId(Guid value)
        : base(value)
    {
    }
}