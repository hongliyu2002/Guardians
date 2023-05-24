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
internal sealed class ChangeCaseStatusCommandHandler : ICommandHandler<ChangeCaseStatusCommand, CaseDto>
{
    private readonly IRepository<Case, CaseId> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<ChangeCaseStatusCommandHandler> _logger;

    public ChangeCaseStatusCommandHandler(IRepository<Case, CaseId> repository, IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, ILogger<ChangeCaseStatusCommandHandler> logger)
    {
        _repository = Guard.Against.Null(repository, nameof(repository));
        Guard.Against.Null(unitOfWorkFactory, nameof(unitOfWorkFactory));
        _unitOfWork = unitOfWorkFactory.CreateUnitOfWork(repository.RepositoryName);
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _logger = Guard.Against.Null(logger, nameof(logger));
    }

    /// <inheritdoc />
    public async Task<Result<CaseDto>> Handle(ChangeCaseStatusCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var @case = await _repository.GetAsync(command.CaseId, cancellationToken);
            if (@case == null || @case.IsDeleted)
            {
                return Result.Fail<CaseDto>(new ExceptionalError("CaseNotFound", new FileNotFoundException($"Case '{command.CaseId}' does not exist or has been deleted")));
            }
            @case.Status = command.Input.Status;
            await _repository.UpdateAsync(@case, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var caseDto = _mapper.Map<CaseDto>(@case);
            return Result.Ok(caseDto);
        }
        catch (Exception ex)
        {
            _unitOfWork.DiscardChanges();
            _logger.LogError(ex, "Error while updating @case '{CaseId}'", command.CaseId);
            return Result.Fail<CaseDto>(new ExceptionalError(ex));
        }
    }
}