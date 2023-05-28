using AutoMapper;
using Fluxera.Extensions.Hosting.Modules.Application;
using Fluxera.Guards;
using Fluxera.Repository;
using Fluxera.Repository.Query;
using Guardians.Application.Contracts.Queries;
using Guardians.Application.Contracts.States;
using Guardians.Domain;
using Guardians.Domain.Shared;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Guardians.Application.Handlers;

[UsedImplicitly]
internal sealed class GetPagedCasesQueryHandler : IQueryHandler<GetPagedCasesQuery, PagedListResultDto<CaseDto>>
{
    private readonly ILogger<GetPagedCasesQueryHandler> _logger;
    private readonly IRepository<Case, CaseId> _repository;
    private readonly QueryOptionsBuilder<Case> _queryOptionsBuilder;
    private readonly IMapper _mapper;

    public GetPagedCasesQueryHandler(IRepository<Case, CaseId> repository, QueryOptionsBuilder<Case> queryOptionsBuilder, IMapper mapper, ILogger<GetPagedCasesQueryHandler> logger)
    {
        _repository = Guard.Against.Null(repository, nameof(repository));
        _queryOptionsBuilder = Guard.Against.Null(queryOptionsBuilder, nameof(queryOptionsBuilder));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _logger = Guard.Against.Null(logger, nameof(logger));
    }

    /// <inheritdoc />
    public async Task<PagedListResultDto<CaseDto>> Handle(GetPagedCasesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var queryOptions = _queryOptionsBuilder.Include(c => c.Scene).OrderByDescending(c => c.ID).Paging(query.PageNo, query.PageSize).Build(cases => cases.AsNoTracking());
            var casesCount = await _repository.CountAsync(c => c.ReportedAt >= query.StartDate && c.ReportedAt < query.EndDate && c.IsDeleted == false, cancellationToken);
            var casesPageCount = (long)Math.Ceiling(casesCount / (double)query.PageSize);
            var cases = await _repository.FindManyAsync(c => c.ReportedAt >= query.StartDate && c.ReportedAt < query.EndDate && c.IsDeleted == false, queryOptions, cancellationToken);
            var caseDtos = _mapper.Map<IReadOnlyList<CaseDto>>(cases);
            return new PagedListResultDto<CaseDto>(query.PageNo, query.PageSize, casesPageCount, casesCount, caseDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while handling {QueryType}", nameof(GetPagedCasesQuery));
            return new PagedListResultDto<CaseDto>();
        }
    }
}