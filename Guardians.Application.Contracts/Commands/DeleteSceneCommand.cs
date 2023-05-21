using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
using Guardians.Domain.Shared;
using JetBrains.Annotations;

namespace Guardians.Application.Contracts.Commands;

[PublicAPI]
public sealed class DeleteSceneCommand : ICommand
{
    public DeleteSceneCommand(SceneId sceneId)
    {
        SceneId = sceneId;
    }

    public SceneId SceneId { get; }
}