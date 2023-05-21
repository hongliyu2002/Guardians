using AutoMapper;
using FluentResults;
using Fluxera.Extensions.Hosting.Modules.Application;
using Fluxera.Guards;
using Fluxera.Repository;
using Guardians.Application.Contracts.Commands;
using Guardians.Application.Contracts.States;
using Guardians.Domain;
using Guardians.Domain.Shared;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Guardians.Application.Handlers;

[UsedImplicitly]
internal sealed class CreateSceneCommandHandler : ICommandHandler<CreateSceneCommand, SceneDto>
{
    private readonly IRepository<Scene, SceneId> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSceneCommandHandler> _logger;

    public CreateSceneCommandHandler(IRepository<Scene, SceneId> repository, IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, ILogger<CreateSceneCommandHandler> logger)
    {
        _repository = Guard.Against.Null(repository, nameof(repository));
        Guard.Against.Null(unitOfWorkFactory, nameof(unitOfWorkFactory));
        _unitOfWork = unitOfWorkFactory.CreateUnitOfWork(repository.RepositoryName);
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _logger = Guard.Against.Null(logger, nameof(logger));
    }

    /// <inheritdoc />
    public async Task<Result<SceneDto>> Handle(CreateSceneCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var scene = new Scene { Title = command.Input.Title };
            await _repository.AddAsync(scene, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            var sceneDto = _mapper.Map<SceneDto>(scene);
            return Result.Ok(sceneDto);
        }
        catch (Exception ex)
        {
            _unitOfWork.DiscardChanges();
            _logger.LogError(ex, "Error while adding scene with title '{Title}'", command.Input.Title);
            return Result.Fail<SceneDto>(new ExceptionalError(ex));
        }
    }
}