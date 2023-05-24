using Fluxera.Guards;
using Guardians.Application.Contracts;
using Guardians.Application.Contracts.States;
using Guardians.Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Guardians.HttpApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/scenes")]
public sealed class ScenesController : ControllerBase
{
    private readonly ISceneApplicationService _sceneAppService;

    public ScenesController(ISceneApplicationService sceneAppService)
    {
        _sceneAppService = Guard.Against.Null(sceneAppService, nameof(sceneAppService));
    }

    [HttpPost]
    public async Task<IActionResult> Create(SceneForCreationDto input)
    {
        var result = await _sceneAppService.CreateSceneAsync(input);
        return StatusCode(result.Code, result);
    }

    [HttpDelete("{sceneID:required}")]
    public async Task<IActionResult> Delete(SceneId sceneID)
    {
        var result = await _sceneAppService.DeleteSceneAsync(sceneID);
        return StatusCode(result.Code, result);
    }

    [HttpGet("{sceneID:required}")]
    public async Task<IActionResult> Get(SceneId sceneID)
    {
        var result = await _sceneAppService.GetSceneAsync(sceneID);
        return StatusCode(result.Code, result);
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var result = await _sceneAppService.ListScenesAsync();
        return StatusCode(result.Code, result);
    }
}