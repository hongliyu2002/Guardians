using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
using Guardians.Application.Contracts.States;
using JetBrains.Annotations;

namespace Guardians.Application.Contracts.Queries;

[PublicAPI]
public sealed class ListPagedCasesQuery : IQuery<PagedListResultDto<CaseDto>>
{
    public ListPagedCasesQuery(string? reporterNo, DateTimeOffset startDate, DateTimeOffset endDate, int pageNo, int pageSize)
    {
        ReporterNo = reporterNo;
        StartDate = startDate == DateTimeOffset.MinValue ? DateTimeOffset.UnixEpoch : startDate;
        EndDate = endDate == DateTimeOffset.MinValue ? DateTimeOffset.Now : endDate;
        PageNo = pageNo < 1 ? 1 : pageNo;
        PageSize = pageSize < 1 ? 10 : pageSize;
    }

    public string? ReporterNo { get; set; }

    public DateTimeOffset StartDate { get; set; }

    public DateTimeOffset EndDate { get; set; }

    public int PageNo { get; set; }

    public int PageSize { get; set; }
}