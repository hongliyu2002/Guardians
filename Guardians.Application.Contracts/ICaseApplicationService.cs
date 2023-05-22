using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
using Guardians.Application.Contracts.States;
using Guardians.Domain.Shared;
using JetBrains.Annotations;

namespace Guardians.Application.Contracts;

[PublicAPI]
public interface ICaseApplicationService : IApplicationService
{
    Task<ResultDto<CaseDto>> CreateCaseAsync(CaseForCreationDto input);

    Task<ResultDto<CaseDto>> UpdateCaseInfoAsync(CaseId caseID, CaseForUpdateDto input);

    Task<ResultDto<CaseDto>> ChangeCaseStatusAsync(CaseId caseID, CaseForStatusChangeDto input);

    Task<ResultDto<CaseId>> DeleteCaseAsync(CaseId caseID);

    Task<ResultDto<CaseDto>> GetCaseAsync(CaseId caseID);

    Task<ResultDto<PagedResultDto<CaseDto>>> ListPagedCasesAsync(string? reporterNo, DateTimeOffset startDate, DateTimeOffset endDate, int pageNo = 1, int pageSize = 10);
}