using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
using Fluxera.Extensions.Http;
using Guardians.Application.Contracts;
using Guardians.Application.Contracts.States;
using Guardians.Domain.Shared;
using JetBrains.Annotations;

namespace Guardians.HttpClient;

[UsedImplicitly]
internal sealed class CaseApplicationServiceClient : HttpClientServiceBase, ICaseApplicationService
{
    public CaseApplicationServiceClient(string name, System.Net.Http.HttpClient httpClient, RemoteService options)
        : base(name, httpClient, options)
    {
    }

    /// <inheritdoc />
    public async Task<ResultDto<CaseDto>> CreateCaseAsync(CaseForCreationDto input)
    {
        var content = input.AsJsonContent();
        var response = await HttpClient.PostAsync("/api/cases", content);
        var result = await response.Content.ReadAsAsync<ResultDto<CaseDto>>();
        return result;
    }

    /// <inheritdoc />
    public async Task<ResultDto<CaseDto>> UpdateCaseInfoAsync(CaseId caseID, CaseForUpdateDto input)
    {
        var content = input.AsJsonContent();
        var response = await HttpClient.PutAsync($"/api/cases/{caseID}", content);
        var result = await response.Content.ReadAsAsync<ResultDto<CaseDto>>();
        return result;
    }

    /// <inheritdoc />
    public async Task<ResultDto<CaseDto>> ChangeCaseStatusAsync(CaseId caseID, CaseForStatusChangeDto input)
    {
        var content = input.AsJsonContent();
        var response = await HttpClient.PutAsync($"/api/cases/{caseID}/status", content);
        var result = await response.Content.ReadAsAsync<ResultDto<CaseDto>>();
        return result;
    }

    /// <inheritdoc />
    public async Task<ResultDto<CaseId>> DeleteCaseAsync(CaseId caseID)
    {
        var response = await HttpClient.DeleteAsync($"/api/cases/{caseID}");
        var result = await response.Content.ReadAsAsync<ResultDto<CaseId>>();
        return result;
    }

    /// <inheritdoc />
    public Task<ResultDto<CaseDto>> GetCaseAsync(CaseId caseID)
    {
        return null;
    }

    /// <inheritdoc />
    public Task<ResultDto<PagedResultDto<CaseDto>>> ListPagedCasesAsync(string? reporterNo, DateTimeOffset startDate, DateTimeOffset endDate, int pageNo = 1, int pageSize = 10)
    {
        return null;
    }
}