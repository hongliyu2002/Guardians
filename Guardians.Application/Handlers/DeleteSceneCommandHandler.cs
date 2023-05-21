using FluentResults;
using Fluxera.Extensions.Hosting.Modules.Application;
using Fluxera.Guards;
using Fluxera.Repository;
using Guardians.Application.Contracts.Commands;
using Guardians.Domain;
using Guardians.Domain.Shared;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Guardians.Application.Handlers;

[UsedImplicitly]
internal sealed class DeleteSceneCommandHandler : ICommandHandler<DeleteSceneCommand>
{
    private readonly IRepository<Scene, SceneId> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteSceneCommandHandler> _logger;

    public DeleteSceneCommandHandler(IRepository<Scene, SceneId> repository, IUnitOfWorkFactory unitOfWorkFactory, ILogger<DeleteSceneCommandHandler> logger)
    {
        _repository = Guard.Against.Null(repository, nameof(repository));
        Guard.Against.Null(unitOfWorkFactory, nameof(unitOfWorkFactory));
        _unitOfWork = unitOfWorkFactory.CreateUnitOfWork(repository.RepositoryName);
        _logger = Guard.Against.Null(logger, nameof(logger));
    }

    public async Task<Result> Handle(DeleteSceneCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var scene = await _repository.GetAsync(command.SceneId, cancellationToken).ConfigureAwait(false);
            if (scene == null || scene.IsDeleted)
            {
                return Result.Fail(new ExceptionalError("SceneNotFound", new InvalidOperationException($"Scene '{command.SceneId}' does not exist or has been deleted")));
            }
            scene.IsDeleted = true;
            await _repository.UpdateAsync(scene, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            _unitOfWork.DiscardChanges();
            _logger.LogError(ex, "Error while deleting scene '{SceneId}'", command.SceneId);
            return Result.Fail(new ExceptionalError(ex));
        }
    }
}