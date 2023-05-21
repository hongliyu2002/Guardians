using FluentResults;
using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
using Fluxera.Guards;
using Guardians.Application.Contracts;
using Guardians.Application.Contracts.Commands;
using Guardians.Application.Contracts.Queries;
using Guardians.Application.Contracts.States;
using Guardians.Domain.Shared;
using JetBrains.Annotations;
using MediatR;

namespace Guardians.Application;

[UsedImplicitly]
internal sealed class SceneApplicationService : ISceneApplicationService
{
    private readonly ISender _sender;

    public SceneApplicationService(ISender sender)
    {
        _sender = Guard.Against.Null(sender, nameof(sender));
    }

    /// <inheritdoc />
    public Task<Result<SceneDto>> CreateSceneAsync(SceneForCreationDto input)
    {
        return _sender.Send(new CreateSceneCommand(input));
    }

    /// <inheritdoc />
    public Task<Result> DeleteSceneAsync(SceneId sceneID)
    {
        return _sender.Send(new DeleteSceneCommand(sceneID));
    }

    /// <inheritdoc />
    public Task<SceneDto?> GetSceneAsync(SceneId sceneID)
    {
        return _sender.Send(new GetSceneQuery(sceneID));
    }

    /// <inheritdoc />
    public Task<ListResultDto<SceneDto>> ListScenesAsync()
    {
        return _sender.Send(new ListScenesQuery());
    }
}