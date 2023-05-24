using System.Collections.ObjectModel;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using Fluxera.Guards;
using Fluxera.Repository;
using Guardians.Domain;
using Guardians.Domain.Shared;
using ReactiveUI;

namespace Guardians.Blazor.ViewModels;

public class ReportViewModel : ReactiveObject
{

    /// <inheritdoc />
    public ReportViewModel(IRepository<Scene, SceneId> repository)
    {
        Guard.Against.Null(repository, nameof(repository));
        var scenesCache = new SourceCache<SceneItemViewModel, Guid>(scene => scene.Id);
        scenesCache.Connect()
                   .AutoRefresh(scene => scene.Title)
                   .AutoRefresh(scene => scene.IsDeleted)
                   .Sort(SortExpressionComparer<SceneItemViewModel>.Ascending(scene => scene.Id))
                   .Bind(out var scenes)
                   .Subscribe();
        Scenes = scenes;
        this.WhenAnyValue(vm => vm.SearchTerm)
            .DistinctUntilChanged()
            .SelectMany(term => repository.FindManyAsync(scene => scene.IsDeleted == false))
            .Subscribe(scenesList => scenesCache.AddOrUpdateWith(scenesList));
    }

    #region Properties

    public ReadOnlyObservableCollection<SceneItemViewModel> Scenes { get; }

    private object? _currentScene;
    public object? CurrentScene
    {
        get => _currentScene;
        set => this.RaiseAndSetIfChanged(ref _currentScene, value);
    }

    private string _searchTerm = string.Empty;
    public string SearchTerm
    {
        get => _searchTerm;
        set => this.RaiseAndSetIfChanged(ref _searchTerm, value);
    }

    #endregion

}