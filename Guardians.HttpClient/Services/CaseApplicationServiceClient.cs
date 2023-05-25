using System.Text;
using Fluxera.Extensions.Http;
using Fluxera.Utilities.Extensions;
using Guardians.Application.Contracts;
using Guardians.Application.Contracts.States;
using Guardians.Domain.Shared;
using JetBrains.Annotations;

namespace Guardians.HttpClient.Services;

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
        try
        {
            var content = input.AsJsonContent();
            var response = await HttpClient.PostAsync("/api/cases", content);
            var result = await response.Content.ReadAsAsync<ResultDto<CaseDto>>();
            return result;
        }
        catch (Exception ex)
        {
            return new ResultDto<CaseDto> { Code = 500, Msg = ex.Message };
        }
    }

    /// <inheritdoc />
    public async Task<ResultDto<CaseDto>> UpdateCaseInfoAsync(CaseId caseID, CaseForUpdateDto input)
    {
        try
        {
            var content = input.AsJsonContent();
            var response = await HttpClient.PutAsync($"/api/cases/{caseID}", content);
            var result = await response.Content.ReadAsAsync<ResultDto<CaseDto>>();
            return result;
        }
        catch (Exception ex)
        {
            return new ResultDto<CaseDto> { Code = 500, Msg = ex.Message };
        }
    }

    /// <inheritdoc />
    public async Task<ResultDto<CaseDto>> ChangeCaseStatusAsync(CaseId caseID, CaseForStatusChangeDto input)
    {
        try
        {
            var content = input.AsJsonContent();
            var response = await HttpClient.PutAsync($"/api/cases/{caseID}/status", content);
            var result = await response.Content.ReadAsAsync<ResultDto<CaseDto>>();
            return result;
        }
        catch (Exception ex)
        {
            return new ResultDto<CaseDto> { Code = 500, Msg = ex.Message };
        }
    }

    /// <inheritdoc />
    public async Task<ResultDto<CaseId>> DeleteCaseAsync(CaseId caseID)
    {
        try
        {
            var response = await HttpClient.DeleteAsync($"/api/cases/{caseID}");
            var result = await response.Content.ReadAsAsync<ResultDto<CaseId>>();
            return result;
        }
        catch (Exception ex)
        {
            return new ResultDto<CaseId> { Code = 500, Msg = ex.Message };
        }
    }

    /// <inheritdoc />
    public async Task<ResultDto<CaseDto>> GetCaseAsync(CaseId caseID)
    {
        try
        {
            var response = await HttpClient.GetAsync($"/api/cases/{caseID}");
            var result = await response.Content.ReadAsAsync<ResultDto<CaseDto>>();
            return result;
        }
        catch (Exception ex)
        {
            return new ResultDto<CaseDto> { Code = 500, Msg = ex.Message };
        }
    }

    /// <inheritdoc />
    public async Task<ResultDto<PagedListResultDto<CaseDto>>> ListPagedCasesAsync(string? reporterNo, DateTimeOffset startDate, DateTimeOffset endDate, int pageNo = 1, int pageSize = 10)
    {
        try
        {
            var query = new StringBuilder();
            query.Append($"?pageNo={pageNo}&pageSize={pageSize}&startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
            if (reporterNo.IsNotNullOrEmpty())
            {
                query.Append($"&reporterNo={reporterNo}");
            }
            var response = await HttpClient.GetAsync($"/api/cases/{query}");
            var result = await response.Content.ReadAsAsync<ResultDto<PagedListResultDto<CaseDto>>>();
            return result;
        }
        catch (Exception ex)
        {
            return new ResultDto<PagedListResultDto<CaseDto>> { Code = 500, Msg = ex.Message };
        }
    }
}