using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
using Guardians.Application.Contracts.States;
using Guardians.Domain.Shared;
using JetBrains.Annotations;

namespace Guardians.Application.Contracts.Commands;

[PublicAPI]
public sealed class ChangeCaseStatusCommand : ICommand<CaseDto>
{
    public ChangeCaseStatusCommand(CaseId caseId, CaseForStatusChangeDto input)
    {
        CaseId = caseId;
        Input = input;
    }

    public CaseId CaseId { get; }

    public CaseForStatusChangeDto Input { get; }
}