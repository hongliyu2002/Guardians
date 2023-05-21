using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
using Guardians.Application.Contracts.States;
using Guardians.Domain.Shared;
using JetBrains.Annotations;

namespace Guardians.Application.Contracts.Queries;

[PublicAPI]
public sealed class GetSceneQuery : IQuery<SceneDto>
{
    public GetSceneQuery(SceneId sceneId)
    {
        SceneId = sceneId;
    }

    public SceneId SceneId { get; }
}