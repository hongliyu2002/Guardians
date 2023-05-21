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

namespace Guardians.Application.Handlers;

[UsedImplicitly]
internal sealed class GetSceneQueryHandler : IQueryHandler<GetSceneQuery, SceneDto?>
{
    private readonly IRepository<Scene, SceneId> _repository;
    private readonly QueryOptionsBuilder<Scene> _queryOptionsBuilder;
    private readonly IMapper _mapper;

    public GetSceneQueryHandler(IRepository<Scene, SceneId> repository, QueryOptionsBuilder<Scene> queryOptionsBuilder, IMapper mapper)
    {
        _repository = Guard.Against.Null(repository, nameof(repository));
        _queryOptionsBuilder = Guard.Against.Null(queryOptionsBuilder, nameof(queryOptionsBuilder));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
    }

    /// <inheritdoc />
    public async Task<SceneDto?> Handle(GetSceneQuery query, CancellationToken cancellationToken)
    {
        var queryOptions = _queryOptionsBuilder.OrderBy(s => s.ID).Build(scenes => scenes.AsNoTracking());
        var scene = await _repository.FindOneAsync(s => s.ID == query.SceneId && s.IsDeleted == false, queryOptions, cancellationToken).ConfigureAwait(false);
        return scene == null ? null : _mapper.Map<SceneDto>(scene);
    }
}