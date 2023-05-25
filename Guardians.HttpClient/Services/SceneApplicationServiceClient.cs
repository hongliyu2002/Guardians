using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
using Fluxera.Extensions.Http;
using Fluxera.Guards;
using Guardians.Application.Contracts;
using Guardians.Application.Contracts.States;
using Guardians.Domain.Shared;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

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
        try
        {
            var content = input.AsJsonContent();
            var response = await HttpClient.PostAsync("/api/scenes", content);
            var result = await response.Content.ReadAsAsync<ResultDto<SceneDto>>();
            return result;
        }
        catch (Exception ex)
        {
            return new ResultDto<SceneDto> { Code = 500, Msg = ex.Message };
        }
    }

    /// <inheritdoc />
    public async Task<ResultDto<SceneId>> DeleteSceneAsync(SceneId sceneID)
    {
        try
        {
            var response = await HttpClient.DeleteAsync($"/api/scenes/{sceneID}");
            var result = await response.Content.ReadAsAsync<ResultDto<SceneId>>();
            return result;
        }
        catch (Exception ex)
        {
            return new ResultDto<SceneId> { Code = 500, Msg = ex.Message };
        }
    }

    /// <inheritdoc />
    public async Task<ResultDto<SceneDto>> GetSceneAsync(SceneId sceneID)
    {
        try
        {
            var response = await HttpClient.GetAsync($"/api/scenes/{sceneID}");
            var result = await response.Content.ReadAsAsync<ResultDto<SceneDto>>();
            return result;
        }
        catch (Exception ex)
        {
            return new ResultDto<SceneDto> { Code = 500, Msg = ex.Message };
        }
    }

    /// <inheritdoc />
    public async Task<ResultDto<ListResultDto<SceneDto>>> ListScenesAsync()
    {
        try
        {
            var response = await HttpClient.GetAsync("/api/scenes");
            var result = await response.Content.ReadAsAsync<ResultDto<ListResultDto<SceneDto>>>();
            return result;
        }
        catch (Exception ex)
        {
            return new ResultDto<ListResultDto<SceneDto>> { Code = 500, Msg = ex.Message };
        }
    }
}