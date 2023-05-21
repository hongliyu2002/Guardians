using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
using Guardians.Application.Contracts.States;
using Guardians.Domain.Shared;
using JetBrains.Annotations;

namespace Guardians.Application.Contracts.Queries;

[PublicAPI]
public sealed class GetCaseQuery : IQuery<CaseDto?>
{
    public GetCaseQuery(CaseId caseId)
    {
        CaseId = caseId;
    }

    public CaseId CaseId { get; }
}