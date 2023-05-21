using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
using Fluxera.Guards;
using Guardians.Application.Contracts.States;
using JetBrains.Annotations;

namespace Guardians.Application.Contracts.Queries;

[PublicAPI]
public sealed class ListPagedCasesQuery : IQuery<PagedResultDto<CaseDto>>
{
    public ListPagedCasesQuery(DateTimeOffset startDate, DateTimeOffset endDate, int pageNo, int pageSize)
    {
        StartDate = startDate;
        EndDate = endDate;
        PageNo = Guard.Against.NegativeOrZero(pageNo, nameof(pageNo));
        PageSize = Guard.Against.NegativeOrZero(pageSize, nameof(pageSize));
    }

    public DateTimeOffset StartDate { get; set; }

    public DateTimeOffset EndDate { get; set; }

    public int PageNo { get; set; }

    public int PageSize { get; set; }
}