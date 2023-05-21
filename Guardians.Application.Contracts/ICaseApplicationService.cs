using FluentResults;
using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
using Guardians.Application.Contracts.States;
using Guardians.Domain.Shared;
using JetBrains.Annotations;

namespace Guardians.Application.Contracts;

[PublicAPI]
public interface ICaseApplicationService : IApplicationService
{
    Task<Result<CaseDto>> CreateCaseAsync(CaseForCreationDto input);

    Task<Result<CaseDto>> UpdateCaseInfoAsync(CaseId caseID, CaseForUpdateDto input);

    Task<Result<CaseDto>> ChangeCaseStatusAsync(CaseId caseID, CaseForStatusChangeDto input);

    Task<Result> DeleteCaseAsync(CaseId caseID);

    Task<Result<CaseDto>> GetCaseAsync(CaseId caseID);

    Task<Result<PagedResultDto<CaseDto>>> ListPagedCasesAsync(DateTime startDate, DateTime endDate, int pageNo, int pageSize);
}