using AutoMapper;
using Fluxera.Extensions.Hosting.Modules.Application;
using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
using Fluxera.Guards;
using Fluxera.Repository;
using Fluxera.Repository.Query;
using Fluxera.Utilities.Extensions;
using Guardians.Application.Contracts.Queries;
using Guardians.Application.Contracts.States;
using Guardians.Domain;
using Guardians.Domain.Shared;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Guardians.Application.Handlers;

[UsedImplicitly]
internal sealed class ListPagedCasesQueryHandler : IQueryHandler<ListPagedCasesQuery, PagedResultDto<CaseDto>>
{
    private readonly IRepository<Case, CaseId> _repository;
    private readonly QueryOptionsBuilder<Case> _queryOptionsBuilder;
    private readonly IMapper _mapper;

    public ListPagedCasesQueryHandler(IRepository<Case, CaseId> repository, QueryOptionsBuilder<Case> queryOptionsBuilder, IMapper mapper)
    {
        _repository = Guard.Against.Null(repository, nameof(repository));
        _queryOptionsBuilder = Guard.Against.Null(queryOptionsBuilder, nameof(queryOptionsBuilder));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
    }

    /// <inheritdoc />
    public async Task<PagedResultDto<CaseDto>> Handle(ListPagedCasesQuery query, CancellationToken cancellationToken)
    {
        var queryOptions = _queryOptionsBuilder.Include(c => c.Scene).OrderByDescending(c => c.ID).Paging(query.PageNo, query.PageSize).Build(cases => cases.AsNoTracking());
        if (query.ReporterNo.IsNullOrEmpty())
        {
            var casesCount = await _repository.CountAsync(c => c.ReportedAt >= query.StartDate && c.ReportedAt < query.EndDate && c.IsDeleted == false, cancellationToken).ConfigureAwait(false);
            var cases = await _repository.FindManyAsync(c => c.ReportedAt >= query.StartDate && c.ReportedAt < query.EndDate && c.IsDeleted == false, queryOptions, cancellationToken).ConfigureAwait(false);
            var caseDtos = _mapper.Map<IReadOnlyList<CaseDto>>(cases);
            return new PagedResultDto<CaseDto>(casesCount, caseDtos);
        }
        else
        {
            var casesCount = await _repository.CountAsync(c => c.ReporterNo == query.ReporterNo && c.ReportedAt >= query.StartDate && c.ReportedAt < query.EndDate && c.IsDeleted == false, cancellationToken).ConfigureAwait(false);
            var cases = await _repository.FindManyAsync(c => c.ReporterNo == query.ReporterNo && c.ReportedAt >= query.StartDate && c.ReportedAt < query.EndDate && c.IsDeleted == false, queryOptions, cancellationToken).ConfigureAwait(false);
            var caseDtos = _mapper.Map<IReadOnlyList<CaseDto>>(cases);
            return new PagedResultDto<CaseDto>(casesCount, caseDtos);
        }
    }
}