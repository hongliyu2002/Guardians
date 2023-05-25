using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
using Fluxera.Extensions.Http;
using Guardians.Application.Contracts;
using Guardians.Application.Contracts.States;
using Guardians.Domain.Shared;
using JetBrains.Annotations;

namespace Guardians.HttpClient.Services;

[UsedImplicitly]
internal sealed class SceneApplicationServiceClient : HttpClientServiceBase, ISceneApplicationService
{
    public SceneApplicationServiceClient(string name, System.Net.Http.HttpClient httpClient, RemoteService options)
        : base(name, httpClient, options)
    {
    }

    /// <inheritdoc />
    public async Task<ResultDto<SceneDto>> CreateSceneAsync(SceneForCreationDto input)
    {
        var content = input.AsJsonContent();
        var response = await HttpClient.PostAsync("/api/scenes", content);
        var result = await response.Content.ReadAsAsync<ResultDto<SceneDto>>();
        return result;
    }

    /// <inheritdoc />
    public async Task<ResultDto<SceneId>> DeleteSceneAsync(SceneId sceneID)
    {
        var response = await HttpClient.DeleteAsync($"/api/scenes/{sceneID}");
        var result = await response.Content.ReadAsAsync<ResultDto<SceneId>>();
        return result;
    }

    /// <inheritdoc />
    public async Task<ResultDto<SceneDto>> GetSceneAsync(SceneId sceneID)
    {
        var response = await HttpClient.GetAsync($"/api/scenes/{sceneID}");
        var result = await response.Content.ReadAsAsync<ResultDto<SceneDto>>();
        return result;
    }

    /// <inheritdoc />
    public async Task<ResultDto<ListResultDto<SceneDto>>> ListScenesAsync()
    {
        var response = await HttpClient.GetAsync("/api/scenes");
        var result = await response.Content.ReadAsAsync<ResultDto<ListResultDto<SceneDto>>>();
        return result;
    }
}