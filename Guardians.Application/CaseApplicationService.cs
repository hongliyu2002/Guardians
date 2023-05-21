using FluentResults;
using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
using Fluxera.Guards;
using Guardians.Application.Contracts;
using Guardians.Application.Contracts.Commands;
using Guardians.Application.Contracts.Queries;
using Guardians.Application.Contracts.States;
using Guardians.Domain.Shared;
using JetBrains.Annotations;
using MediatR;

namespace Guardians.Application;

[UsedImplicitly]
internal sealed class CaseApplicationService : ICaseApplicationService
{
    private readonly ISender _sender;

    public CaseApplicationService(ISender sender)
    {
        _sender = Guard.Against.Null(sender, nameof(sender));
    }

    /// <inheritdoc />
    public Task<Result<CaseDto>> CreateCaseAsync(CaseForCreationDto input)
    {
        return _sender.Send(new CreateCaseCommand(input));
    }

    /// <inheritdoc />
    public Task<Result<CaseDto>> UpdateCaseInfoAsync(CaseId caseID, CaseForUpdateDto input)
    {
        return _sender.Send(new UpdateCaseInfoCommand(caseID, input));
    }

    /// <inheritdoc />
    public Task<Result<CaseDto>> ChangeCaseStatusAsync(CaseId caseID, CaseForStatusChangeDto input)
    {
        return _sender.Send(new ChangeCaseStatusCommand(caseID, input));
    }

    /// <inheritdoc />
    public Task<Result> DeleteCaseAsync(CaseId caseID)
    {
        return _sender.Send(new DeleteCaseCommand(caseID));
    }

    /// <inheritdoc />
    public Task<CaseDto?> GetCaseAsync(CaseId caseID)
    {
        return _sender.Send(new GetCaseQuery(caseID));
    }

    /// <inheritdoc />
    public Task<PagedResultDto<CaseDto>> ListPagedCasesAsync(DateTimeOffset startDate, DateTimeOffset endDate, int pageNo, int pageSize)
    {
        return _sender.Send(new ListPagedCasesQuery(startDate, endDate, pageNo, pageSize));
    }
}