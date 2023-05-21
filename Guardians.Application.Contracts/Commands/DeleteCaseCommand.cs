using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
using Guardians.Domain.Shared;
using JetBrains.Annotations;

namespace Guardians.Application.Contracts.Commands;

[PublicAPI]
public sealed class DeleteCaseCommand : ICommand
{
    public DeleteCaseCommand(CaseId caseId)
    {
        CaseId = caseId;
    }

    public CaseId CaseId { get; }
}