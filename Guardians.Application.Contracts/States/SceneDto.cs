using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
using Guardians.Domain.Shared;
using JetBrains.Annotations;

namespace Guardians.Application.Contracts.States;

[PublicAPI]
[Serializable]
public sealed class SceneDto : EntityDto<SceneId>
{
    public string Title { get; set; } = default!;
}