using DynamicData;
using Guardians.Domain;

namespace Guardians.Blazor.ViewModels;

public static class ScenesCacheExtensions
{
    public static void AddOrUpdateWith(this SourceCache<SceneItemViewModel, Guid> sourceCache, Scene scene)
    {
        sourceCache.Edit(updater =>
                         {
                             var current = updater.Lookup(scene.ID.Value);
                             if (current.HasValue)
                             {
                                 current.Value.UpdateWith(scene);
                             }
                             else
                             {
                                 updater.AddOrUpdate(new SceneItemViewModel(scene));
                             }
                         });
    }

    public static void AddOrUpdateWith(this SourceCache<SceneItemViewModel, Guid> sourceCache, IReadOnlyCollection<Scene> scenes)
    {
        sourceCache.Edit(updater =>
                         {
                             foreach (var scene in scenes)
                             {
                                 var current = updater.Lookup(scene.ID.Value);
                                 if (current.HasValue)
                                 {
                                     current.Value.UpdateWith(scene);
                                 }
                                 else
                                 {
                                     updater.AddOrUpdate(new SceneItemViewModel(scene));
                                 }
                             }
                         });
    }
}