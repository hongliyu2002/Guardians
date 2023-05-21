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
internal sealed class DeleteCaseCommandHandler : ICommandHandler<DeleteCaseCommand>
{
    private readonly IRepository<Case, CaseId> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteCaseCommandHandler> _logger;

    public DeleteCaseCommandHandler(IRepository<Case, CaseId> repository, IUnitOfWorkFactory unitOfWorkFactory, ILogger<DeleteCaseCommandHandler> logger)
    {
        _repository = Guard.Against.Null(repository, nameof(repository));
        Guard.Against.Null(unitOfWorkFactory, nameof(unitOfWorkFactory));
        _unitOfWork = unitOfWorkFactory.CreateUnitOfWork(repository.RepositoryName);
        _logger = Guard.Against.Null(logger, nameof(logger));
    }

    public async Task<Result> Handle(DeleteCaseCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var @case = await _repository.GetAsync(command.CaseId, cancellationToken).ConfigureAwait(false);
            if (@case == null || @case.IsDeleted)
            {
                return Result.Fail(new ExceptionalError("CaseNotFound", new FileNotFoundException($"Case '{command.CaseId}' does not exist or has been deleted")));
            }
            @case.IsDeleted = true;
            await _repository.UpdateAsync(@case, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            _unitOfWork.DiscardChanges();
            _logger.LogError(ex, "Error while deleting case '{CaseId}'", command.CaseId);
            return Result.Fail(new ExceptionalError(ex));
        }
    }
}