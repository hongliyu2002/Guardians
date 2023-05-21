using Fluxera.Entity;
using Fluxera.Extensions.Hosting.Modules.Domain.Shared.Model;
using Guardians.Domain.Shared;
using JetBrains.Annotations;

namespace Guardians.Domain;

[PublicAPI]
public sealed class Scene : AggregateRoot<Scene, SceneId>, ISoftDeleteObject
{
    public string Title { get; set; } = default!;

    public bool IsDeleted { get; set; }
}