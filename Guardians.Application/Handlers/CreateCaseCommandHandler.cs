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
internal sealed class CreateCaseCommandHandler : ICommandHandler<CreateCaseCommand, CaseDto>
{
    private readonly IRepository<Case, CaseId> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCaseCommandHandler> _logger;

    public CreateCaseCommandHandler(IRepository<Case, CaseId> repository, IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, ILogger<CreateCaseCommandHandler> logger)
    {
        _repository = Guard.Against.Null(repository, nameof(repository));
        Guard.Against.Null(unitOfWorkFactory, nameof(unitOfWorkFactory));
        _unitOfWork = unitOfWorkFactory.CreateUnitOfWork(repository.RepositoryName);
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _logger = Guard.Against.Null(logger, nameof(logger));
    }

    /// <inheritdoc />
    public async Task<Result<CaseDto>> Handle(CreateCaseCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var @case = new Case
                        {
                            SceneID = command.Input.SceneID,
                            ReportedAt = DateTimeOffset.Now,
                            ReporterNo = command.Input.ReporterNo,
                            ReporterName = command.Input.ReporterName,
                            ReporterMobile = command.Input.ReporterMobile,
                            Status = CaseStatus.Reviewing
                        };
            await _repository.AddAsync(@case, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            var caseDto = _mapper.Map<CaseDto>(@case);
            return Result.Ok(caseDto);
        }
        catch (Exception ex)
        {
            _unitOfWork.DiscardChanges();
            _logger.LogError(ex, "Error while adding case with scene ID '{SceneID}'", command.Input.SceneID);
            return Result.Fail<CaseDto>(new ExceptionalError(ex));
        }
    }
}