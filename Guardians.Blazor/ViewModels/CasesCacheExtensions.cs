using DynamicData;
using Guardians.Application.Contracts.States;

namespace Guardians.Blazor.ViewModels;

public static class CasesCacheExtensions
{
    public static void AddOrUpdateWith(this SourceCache<CaseItemViewModel, Guid> sourceCache, CaseDto @case)
    {
        sourceCache.Edit(updater =>
                         {
                             var current = updater.Lookup(@case.ID.Value);
                             if (current.HasValue)
                             {
                                 current.Value.UpdateWith(@case);
                             }
                             else
                             {
                                 updater.AddOrUpdate(new CaseItemViewModel(@case));
                             }
                         });
    }

    public static void AddOrUpdateWith(this SourceCache<CaseItemViewModel, Guid> sourceCache, IReadOnlyList<CaseDto> cases)
    {
        sourceCache.Edit(updater =>
                         {
                             foreach (var @case in cases)
                             {
                                 var current = updater.Lookup(@case.ID.Value);
                                 if (current.HasValue)
                                 {
                                     current.Value.UpdateWith(@case);
                                 }
                                 else
                                 {
                                     updater.AddOrUpdate(new CaseItemViewModel(@case));
                                 }
                             }
                         });
    }
}