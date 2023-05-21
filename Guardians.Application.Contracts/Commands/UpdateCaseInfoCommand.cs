using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
using Guardians.Application.Contracts.States;
using Guardians.Domain.Shared;
using JetBrains.Annotations;

namespace Guardians.Application.Contracts.Commands;

[PublicAPI]
public sealed class UpdateCaseInfoCommand : ICommand<CaseDto>
{
    public UpdateCaseInfoCommand(CaseId caseId, CaseForUpdateDto input)
    {
        CaseId = caseId;
        Input = input;
    }

    public CaseId CaseId { get; }

    public CaseForUpdateDto Input { get; }
}