using AutoMapper;
using Fluxera.Extensions.Hosting.Modules.Application;
using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
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
internal sealed class ListScenesQueryHandler : IQueryHandler<ListScenesQuery, ListResultDto<SceneDto>>
{
    private readonly ILogger<ListScenesQueryHandler> _logger;
    private readonly IRepository<Scene, SceneId> _repository;
    private readonly QueryOptionsBuilder<Scene> _queryOptionsBuilder;
    private readonly IMapper _mapper;

    public ListScenesQueryHandler(IRepository<Scene, SceneId> repository, QueryOptionsBuilder<Scene> queryOptionsBuilder, IMapper mapper, ILogger<ListScenesQueryHandler> logger)
    {
        _repository = Guard.Against.Null(repository, nameof(repository));
        _queryOptionsBuilder = Guard.Against.Null(queryOptionsBuilder, nameof(queryOptionsBuilder));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _logger = Guard.Against.Null(logger, nameof(logger));
    }

    /// <inheritdoc />
    public async Task<ListResultDto<SceneDto>> Handle(ListScenesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var queryOptions = _queryOptionsBuilder.OrderBy(s => s.ID).Build(scenes => scenes.AsNoTracking());
            var scenes = await _repository.FindManyAsync(scene => scene.IsDeleted == false, queryOptions, cancellationToken);
            var sceneDtos = _mapper.Map<IReadOnlyList<SceneDto>>(scenes);
            return new ListResultDto<SceneDto>(sceneDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while handling {QueryType}", nameof(ListScenesQuery));
            return new ListResultDto<SceneDto>();
        }
    }
}