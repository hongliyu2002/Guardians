using Fluxera.Repository;
using Guardians.Domain.Shared;
using JetBrains.Annotations;

namespace Guardians.Domain;

[PublicAPI]
public interface ISceneRepository : IRepository<Scene, SceneId>
{
}

[UsedImplicitly]
internal sealed class SceneRepository : Repository<Scene, SceneId>, ISceneRepository
{
    /// <inheritdoc />
    public SceneRepository(IRepository<Scene, SceneId> innerRepository)
        : base(innerRepository)
    {
    }
}