using FluentResults;
using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
using Guardians.Application.Contracts.States;
using Guardians.Domain.Shared;
using JetBrains.Annotations;

namespace Guardians.Application.Contracts;

[PublicAPI]
public interface ISceneApplicationService : IApplicationService
{
    Task<Result<SceneDto>> CreateSceneAsync(SceneForCreationDto input);

    Task<Result> DeleteSceneAsync(SceneId sceneID);

    Task<Result<SceneDto>> GetSceneAsync(SceneId sceneID);

    Task<Result<ListResultDto<SceneDto>>> ListScenesAsync();
}