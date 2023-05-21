using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
using Guardians.Application.Contracts.States;
using JetBrains.Annotations;

namespace Guardians.Application.Contracts.Commands;

[PublicAPI]
public sealed class CreateSceneCommand : ICommand<SceneDto>
{
    public CreateSceneCommand(SceneForCreationDto input)
    {
        Input = input;
    }

    public SceneForCreationDto Input { get; }
}