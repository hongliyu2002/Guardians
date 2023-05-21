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

namespace Guardians.Application.Handlers;

[UsedImplicitly]
internal sealed class ListScenesQueryHandler : IQueryHandler<ListScenesQuery, ListResultDto<SceneDto>>
{
    private readonly IRepository<Scene, SceneId> _repository;
    private readonly QueryOptionsBuilder<Scene> _queryOptionsBuilder;
    private readonly IMapper _mapper;

    public ListScenesQueryHandler(IRepository<Scene, SceneId> repository, QueryOptionsBuilder<Scene> queryOptionsBuilder, IMapper mapper)
    {
        _repository = Guard.Against.Null(repository, nameof(repository));
        _queryOptionsBuilder = Guard.Against.Null(queryOptionsBuilder, nameof(queryOptionsBuilder));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
    }

    /// <inheritdoc />
    public async Task<ListResultDto<SceneDto>> Handle(ListScenesQuery query, CancellationToken cancellationToken)
    {
        var queryOptions = _queryOptionsBuilder.OrderBy(scene => scene.ID).Build(scenes => scenes.AsNoTracking());
        var scenes = await _repository.FindManyAsync(scene => scene.IsDeleted == false, queryOptions, cancellationToken).ConfigureAwait(false);
        var sceneDtos = _mapper.Map<IReadOnlyList<SceneDto>>(scenes);
        return new ListResultDto<SceneDto>(sceneDtos);
    }
}