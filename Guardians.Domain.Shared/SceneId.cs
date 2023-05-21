using Fluxera.StronglyTypedId;
using JetBrains.Annotations;

namespace Guardians.Domain.Shared;

[PublicAPI]
public sealed class SceneId : StronglyTypedId<SceneId, Guid>
{
    public SceneId(Guid value)
        : base(value)
    {
    }
}