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
[Route("api/cases")]
public sealed class CasesController : ControllerBase
{
    private readonly ICaseApplicationService _caseAppService;

    public CasesController(ICaseApplicationService caseAppService)
    {
        _caseAppService = Guard.Against.Null(caseAppService, nameof(caseAppService));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CaseForCreationDto input)
    {
        var result = await _caseAppService.CreateCaseAsync(input).ConfigureAwait(false);
        return result.IsFailed ? result.ToActionResult() : CreatedAtAction(nameof(Get), new { caseID = result.Value.ID }, result.Value);
    }

    [HttpPut("{caseID:required}")]
    public async Task<IActionResult> UpdateInfo(CaseId caseID, CaseForUpdateDto input)
    {
        var result = await _caseAppService.UpdateCaseInfoAsync(caseID, input).ConfigureAwait(false);
        return result.IsFailed ? result.ToActionResult() : Ok(result.Value);
    }

    [HttpPut("{caseID:required}/status")]
    public async Task<IActionResult> ChangeStatus(CaseId caseID, CaseForStatusChangeDto input)
    {
        var result = await _caseAppService.ChangeCaseStatusAsync(caseID, input).ConfigureAwait(false);
        return result.IsFailed ? result.ToActionResult() : Ok(result.Value);
    }

    [HttpDelete("{caseID:required}")]
    public async Task<IActionResult> Delete(CaseId caseID)
    {
        var result = await _caseAppService.DeleteCaseAsync(caseID).ConfigureAwait(false);
        return result.IsFailed ? result.ToActionResult() : NoContent();
    }

    [HttpGet("{caseID:required}")]
    public async Task<IActionResult> Get(CaseId caseID)
    {
        var caseDto = await _caseAppService.GetCaseAsync(caseID).ConfigureAwait(false);
        return caseDto.IsNull() ? NotFound() : Ok(caseDto);
    }

    [HttpGet]
    public async Task<IActionResult> ListPaged(DateTimeOffset startDate, DateTimeOffset endDate, int pageNo, int pageSize)
    {
        var caseDtos = await _caseAppService.ListPagedCasesAsync(startDate, endDate, pageNo, pageSize).ConfigureAwait(false);
        return Ok(caseDtos);
    }
}