using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
using Guardians.Application.Contracts.States;
using Guardians.Domain.Shared;
using JetBrains.Annotations;

namespace Guardians.Application.Contracts;

[PublicAPI]
public interface ISceneApplicationService : IApplicationService
{
    Task<ResultDto<SceneDto>> CreateSceneAsync(SceneForCreationDto input);

    Task<ResultDto<SceneId>> DeleteSceneAsync(SceneId sceneID);

    Task<ResultDto<SceneDto>> GetSceneAsync(SceneId sceneID);

    Task<ResultDto<ListResultDto<SceneDto>>> ListScenesAsync();
}