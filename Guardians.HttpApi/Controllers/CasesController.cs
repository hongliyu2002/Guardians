using Fluxera.Guards;
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
        var result = await _caseAppService.CreateCaseAsync(input);
        return StatusCode(result.Code, result);
    }

    [HttpPut("{caseID:required}")]
    public async Task<IActionResult> UpdateInfo(CaseId caseID, CaseForUpdateDto input)
    {
        var result = await _caseAppService.UpdateCaseInfoAsync(caseID, input);
        return StatusCode(result.Code, result);
    }

    [HttpPut("{caseID:required}/status")]
    public async Task<IActionResult> ChangeStatus(CaseId caseID, CaseForStatusChangeDto input)
    {
        var result = await _caseAppService.ChangeCaseStatusAsync(caseID, input);
        return StatusCode(result.Code, result);
    }

    [HttpDelete("{caseID:required}")]
    public async Task<IActionResult> Delete(CaseId caseID)
    {
        var result = await _caseAppService.DeleteCaseAsync(caseID);
        return StatusCode(result.Code, result);
    }

    [HttpGet("{caseID:required}")]
    public async Task<IActionResult> Get(CaseId caseID)
    {
        var result = await _caseAppService.GetCaseAsync(caseID);
        return StatusCode(result.Code, result);
    }

    [HttpGet]
    public async Task<IActionResult> ListPaged(string? reporterNo, DateTimeOffset startDate, DateTimeOffset endDate, int pageNo, int pageSize)
    {
        var result = await _caseAppService.ListPagedCasesAsync(reporterNo, startDate, endDate, pageNo, pageSize);
        return StatusCode(result.Code, result);
    }
}