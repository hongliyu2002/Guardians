﻿using System.Net;
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
    public async Task<ResultDto<SceneDto>> CreateSceneAsync(SceneForCreationDto input)
    {
        var result = await _sender.Send(new CreateSceneCommand(input));
        if (result.IsFailed)
        {
            return new ResultDto<SceneDto>
                   {
                       Code = (int)HttpStatusCode.InternalServerError,
                       Msg = result.Errors.First().Message,
                       Data = null
                   };
        }
        return new ResultDto<SceneDto>
               {
                   Code = (int)HttpStatusCode.OK,
                   Msg = HttpStatusCode.OK.ToString(),
                   Data = result.Value
               };
    }

    /// <inheritdoc />
    public async Task<ResultDto<SceneId>> DeleteSceneAsync(SceneId sceneID)
    {
        var result = await _sender.Send(new DeleteSceneCommand(sceneID));
        if (result.IsFailed)
        {
            var firstMessage = result.Errors.First().Message;
            if (firstMessage == "SceneNotFound")
            {
                return new ResultDto<SceneId>
                       {
                           Code = (int)HttpStatusCode.NotFound,
                           Msg = HttpStatusCode.NotFound.ToString(),
                           Data = null
                       };
            }
            return new ResultDto<SceneId>
                   {
                       Code = (int)HttpStatusCode.InternalServerError,
                       Msg = firstMessage,
                       Data = null
                   };
        }
        return new ResultDto<SceneId>
               {
                   Code = (int)HttpStatusCode.OK,
                   Msg = HttpStatusCode.OK.ToString(),
                   Data = sceneID
               };
    }

    /// <inheritdoc />
    public async Task<ResultDto<SceneDto>> GetSceneAsync(SceneId sceneID)
    {
        var scene = await _sender.Send(new GetSceneQuery(sceneID));
        if (scene == null)
        {
            return new ResultDto<SceneDto>
                   {
                       Code = (int)HttpStatusCode.NotFound,
                       Msg = HttpStatusCode.NotFound.ToString(),
                       Data = null
                   };
        }
        return new ResultDto<SceneDto>
               {
                   Code = (int)HttpStatusCode.OK,
                   Msg = HttpStatusCode.OK.ToString(),
                   Data = scene
               };
    }

    /// <inheritdoc />
    public async Task<ResultDto<ListResultDto<SceneDto>>> ListScenesAsync()
    {
        var scenes = await _sender.Send(new ListScenesQuery());
        return new ResultDto<ListResultDto<SceneDto>>
               {
                   Code = (int)HttpStatusCode.OK,
                   Msg = HttpStatusCode.OK.ToString(),
                   Data = scenes
               };
    }
}