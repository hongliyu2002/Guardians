using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using Fluxera.Guards;
using Fluxera.Utilities.Extensions;
using Guardians.Application.Contracts;
using Guardians.Application.Contracts.States;
using Guardians.Domain.Shared;
using ReactiveUI;

namespace Guardians.Blazor.ViewModels;

public class ReportViewModel : ReactiveObject
{
    /// <inheritdoc />
    public ReportViewModel(ISceneApplicationService sceneAppService, ICaseApplicationService caseAppService)
    {
        SceneAppService = Guard.Against.Null(sceneAppService, nameof(sceneAppService));
        CaseAppService = Guard.Against.Null(caseAppService, nameof(caseAppService));
        var scenesCache = new SourceCache<SceneItemViewModel, Guid>(scene => scene.Id);
        scenesCache.Connect()
                   .AutoRefresh(scene => scene.Title)
                   .Sort(SortExpressionComparer<SceneItemViewModel>.Ascending(scene => scene.Id))
                   .Bind(out var scenes)
                   .Subscribe();
        Scenes = scenes;
        this.WhenAnyValue(vm => vm.SearchTerm)
            .Where(term => term.IsNotNullOrEmpty())
            .DistinctUntilChanged()
            .SelectMany(_ => SceneAppService.ListScenesAsync())
            .Where(result => result.Data != null)
            .Select(result => result.Data!.Items)
            .Subscribe(scenesList => scenesCache.AddOrUpdateWith(scenesList));
        SubmitCaseCommand = ReactiveCommand.CreateFromTask(SubmitCaseAsync, CanSubmitCase);
    }

    #region Properties

    public ISceneApplicationService SceneAppService { get; }
    
    public ICaseApplicationService CaseAppService { get; }

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

    private string _reporterNo = string.Empty;
    public string ReporterNo
    {
        get => _reporterNo;
        set => this.RaiseAndSetIfChanged(ref _reporterNo, value);
    }

    private string _reporterName = string.Empty;
    public string ReporterName
    {
        get => _reporterName;
        set => this.RaiseAndSetIfChanged(ref _reporterName, value);
    }

    private string _reporterMobile = string.Empty;
    public string ReporterMobile
    {
        get => _reporterMobile;
        set => this.RaiseAndSetIfChanged(ref _reporterMobile, value);
    }

    #endregion

    #region Interactions

    public Interaction<(string Message, bool Success), Unit> ShowSubmitCaseResultInteraction { get; } = new();

    #endregion

    #region Commands

    public ReactiveCommand<Unit, Unit> SubmitCaseCommand { get; }

    public IObservable<bool> CanSubmitCase =>
        this.WhenAnyValue(vm => vm.CurrentScene, vm => vm.ReporterNo, vm => vm.ReporterName, vm => vm.ReporterMobile)
            .Select(data => data.Item1 != null && data.Item2.IsNotNullOrEmpty() && data.Item3.IsNotNullOrEmpty() && data.Item4.IsNotNullOrEmpty());

    public bool CannotSubmitCase => CurrentScene == null || ReporterNo.IsNullOrEmpty() || ReporterName.IsNullOrEmpty() || ReporterMobile.IsNullOrEmpty();

    private async Task SubmitCaseAsync()
    {
        var result = await CaseAppService.CreateCaseAsync(new CaseForCreationDto
                                                          {
                                                              SceneID = new SceneId(((SceneItemViewModel)CurrentScene!).Id),
                                                              ReporterNo = ReporterNo,
                                                              ReporterName = ReporterName,
                                                              ReporterMobile = ReporterMobile
                                                          });
        await ShowSubmitCaseResultInteraction.Handle(result.Code == 200 ? (Message: "上报成功", Success: true) : (Message: "上报失败，请重试", Success: false));
    }

    #endregion

}