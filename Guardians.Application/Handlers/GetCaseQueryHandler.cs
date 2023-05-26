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
internal sealed class GetCaseQueryHandler : IQueryHandler<GetCaseQuery, CaseDto?>
{
    private readonly IRepository<Case, CaseId> _repository;
    private readonly QueryOptionsBuilder<Case> _queryOptionsBuilder;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCaseQueryHandler> _logger;
    
    public GetCaseQueryHandler(IRepository<Case, CaseId> repository, QueryOptionsBuilder<Case> queryOptionsBuilder, IMapper mapper, ILogger<GetCaseQueryHandler> logger)
    {
        _repository = Guard.Against.Null(repository, nameof(repository));
        _queryOptionsBuilder = Guard.Against.Null(queryOptionsBuilder, nameof(queryOptionsBuilder));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _logger = Guard.Against.Null(logger, nameof(logger));
    }

    /// <inheritdoc />
    public async Task<CaseDto?> Handle(GetCaseQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var queryOptions = _queryOptionsBuilder.Include(c => c.Scene).Build(cases => cases.AsNoTracking());
            var @case = await _repository.FindOneAsync(c => c.ID == query.CaseId && c.IsDeleted == false, queryOptions, cancellationToken);
            return @case == null ? null : _mapper.Map<CaseDto>(@case);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting {QueryType}", nameof(GetCaseQuery));
            return null;
        }
    }
}