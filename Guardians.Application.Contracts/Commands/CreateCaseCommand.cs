using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
using Guardians.Application.Contracts.States;
using JetBrains.Annotations;

namespace Guardians.Application.Contracts.Commands;

[PublicAPI]
public sealed class CreateCaseCommand : ICommand<CaseDto>
{
    public CreateCaseCommand(CaseForCreationDto input)
    {
        Input = input;
    }

    public CaseForCreationDto Input { get; }
}