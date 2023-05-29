using System.Net;
using System.Text;
using System.Text.Json;
using Fluxera.Guards;
using Guardians.Application.Contracts;
using Guardians.Application.Contracts.Commands;
using Guardians.Application.Contracts.Queries;
using Guardians.Application.Contracts.States;
using Guardians.Application.Contracts.Utils;
using Guardians.Domain.Shared;
using JetBrains.Annotations;
using MediatR;
using Newtonsoft.Json;

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
    public async Task<ResultDto<CaseDto>> CreateCaseAsync(CaseForCreationDto input)
    {
        var result = await _sender.Send(new CreateCaseCommand(input));
        if (result.IsFailed)
        {
            return new ResultDto<CaseDto>
                   {
                       Code = (int)HttpStatusCode.InternalServerError,
                       Msg = result.Errors.First().Message,
                       Data = null
                   };
        }
        return new ResultDto<CaseDto>
               {
                   Code = (int)HttpStatusCode.OK,
                   Msg = HttpStatusCode.OK.ToString(),
                   Data = result.Value
               };
    }

    /// <inheritdoc />
    public async Task<ResultDto<CaseDto>> UpdateCaseInfoAsync(CaseId caseID, CaseForUpdateDto input)
    {
        var result = await _sender.Send(new UpdateCaseInfoCommand(caseID, input));
        if (result.IsFailed)
        {
            var firstMessage = result.Errors.First().Message;
            if (firstMessage == "CaseNotFound")
            {
                return new ResultDto<CaseDto>
                       {
                           Code = (int)HttpStatusCode.NotFound,
                           Msg = HttpStatusCode.NotFound.ToString(),
                           Data = null
                       };
            }
            return new ResultDto<CaseDto>
                   {
                       Code = (int)HttpStatusCode.InternalServerError,
                       Msg = firstMessage,
                       Data = null
                   };
        }
        return new ResultDto<CaseDto>
               {
                   Code = (int)HttpStatusCode.OK,
                   Msg = HttpStatusCode.OK.ToString(),
                   Data = result.Value
               };
    }

    /// <inheritdoc />
    public async Task<ResultDto<CaseDto>> ChangeCaseStatusAsync(CaseId caseID, CaseForStatusChangeDto input)
    {
        var result = await _sender.Send(new ChangeCaseStatusCommand(caseID, input));
        if (result.IsFailed)
        {
            var firstMessage = result.Errors.First().Message;
            if (firstMessage == "CaseNotFound")
            {
                return new ResultDto<CaseDto>
                       {
                           Code = (int)HttpStatusCode.NotFound,
                           Msg = HttpStatusCode.NotFound.ToString(),
                           Data = null
                       };
            }
            return new ResultDto<CaseDto>
                   {
                       Code = (int)HttpStatusCode.InternalServerError,
                       Msg = firstMessage,
                       Data = null
                   };
        }
        return new ResultDto<CaseDto>
               {
                   Code = (int)HttpStatusCode.OK,
                   Msg = HttpStatusCode.OK.ToString(),
                   Data = result.Value
               };
    }

    /// <inheritdoc />
    public async Task<ResultDto<CaseId>> DeleteCaseAsync(CaseId caseID)
    {
        var result = await _sender.Send(new DeleteCaseCommand(caseID));
        if (result.IsFailed)
        {
            var firstMessage = result.Errors.First().Message;
            if (firstMessage == "CaseNotFound")
            {
                return new ResultDto<CaseId>
                       {
                           Code = (int)HttpStatusCode.NotFound,
                           Msg = HttpStatusCode.NotFound.ToString(),
                           Data = null
                       };
            }
            return new ResultDto<CaseId>
                   {
                       Code = (int)HttpStatusCode.InternalServerError,
                       Msg = firstMessage,
                       Data = null
                   };
        }
        return new ResultDto<CaseId>
               {
                   Code = (int)HttpStatusCode.OK,
                   Msg = HttpStatusCode.OK.ToString(),
                   Data = caseID
               };
    }

    /// <inheritdoc />
    public async Task<ResultDto<CaseDto>> GetCaseAsync(CaseId caseID)
    {
        var @case = await _sender.Send(new GetCaseQuery(caseID));
        if (@case == null)
        {
            return new ResultDto<CaseDto>
                   {
                       Code = (int)HttpStatusCode.NotFound,
                       Msg = HttpStatusCode.NotFound.ToString(),
                       Data = null
                   };
        }
        return new ResultDto<CaseDto>
               {
                   Code = (int)HttpStatusCode.OK,
                   Msg = HttpStatusCode.OK.ToString(),
                   Data = @case
               };
    }

    /// <inheritdoc />
    public async Task<ResultDto<PagedListResultDto<CaseDto>>> ListPagedCasesAsync(string? reporterNo, DateTimeOffset startDate, DateTimeOffset endDate, int pageNo = 1, int pageSize = 10)
    {
        var cases = await _sender.Send(new ListPagedCasesQuery(reporterNo, startDate, endDate, pageNo, pageSize));
        return new ResultDto<PagedListResultDto<CaseDto>>
               {
                   Code = (int)HttpStatusCode.OK,
                   Msg = HttpStatusCode.OK.ToString(),
                   Data = cases
               };
    }

    /// <inheritdoc />
    public async Task<ResultDto<PagedListResultDto<CaseDto>>> GetPagedCasesAsync(EncryptedQueryDto input)
    {
        var queryJson = Encryptor.DecryptData(input.Param, Encryptor.DailyPublicKeyBase64, Encoding.UTF8);
        var queryDto = JsonConvert.DeserializeObject<QueryDto>(queryJson);
        if (queryDto == null)
        {
            return new ResultDto<PagedListResultDto<CaseDto>>
                   {
                       Code = (int)HttpStatusCode.BadRequest,
                       Msg = HttpStatusCode.BadRequest.ToString(),
                       Data = null
                   };
        }
        var cases = await _sender.Send(new GetPagedCasesQuery(queryDto.StartDate, queryDto.EndDate, queryDto.PageNo, queryDto.PageSize));
        return new ResultDto<PagedListResultDto<CaseDto>>
               {
                   Code = (int)HttpStatusCode.OK,
                   Msg = HttpStatusCode.OK.ToString(),
                   Data = cases
               };
    }
}