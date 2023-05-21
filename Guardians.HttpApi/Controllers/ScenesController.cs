using FluentResults.Extensions.AspNetCore;
using Fluxera.Guards;
using Fluxera.Utilities.Extensions;
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
        var result = await _sceneAppService.CreateSceneAsync(input).ConfigureAwait(false);
        return result.IsFailed ? result.ToActionResult() : CreatedAtAction(nameof(Get), new { sceneID = result.Value.ID }, result.Value);
    }

    [HttpDelete("{sceneID:required}")]
    public async Task<IActionResult> Delete(SceneId sceneID)
    {
        var result = await _sceneAppService.DeleteSceneAsync(sceneID).ConfigureAwait(false);
        return result.IsFailed ? result.ToActionResult() : NoContent();
    }

    [HttpGet("{sceneID:required}")]
    public async Task<IActionResult> Get(SceneId sceneID)
    {
        var sceneDto = await _sceneAppService.GetSceneAsync(sceneID).ConfigureAwait(false);
        return sceneDto.IsNull() ? NotFound() : Ok(sceneDto);
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var sceneDtos = await _sceneAppService.ListScenesAsync().ConfigureAwait(false);
        return Ok(sceneDtos);
    }
}