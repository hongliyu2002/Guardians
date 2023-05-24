using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
using JetBrains.Annotations;

namespace Guardians.Application.Contracts.States;

[PublicAPI]
[Serializable]
public class PagedListResultDto<T> : ListResultDto<T>, IHasTotalCount
{
    public PagedListResultDto()
    {
    }

    public PagedListResultDto(int pageNo, int pageSize, long totalPage, long totalCount, IReadOnlyList<T> items)
        : base(items)
    {
        PageNo = pageNo;
        PageSize = pageSize;
        TotalPage = totalPage;
        TotalCount = totalCount;
    }

    public int PageNo { get; set; }

    public int PageSize { get; set; }

    public long TotalPage { get; set; }

    public long TotalCount { get; set; }
}