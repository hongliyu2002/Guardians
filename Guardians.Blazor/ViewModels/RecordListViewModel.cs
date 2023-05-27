using System.Collections.ObjectModel;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using Fluxera.Guards;
using Fluxera.Utilities.Extensions;
using Guardians.Application.Contracts;
using ReactiveUI;

namespace Guardians.Blazor.ViewModels;

public class RecordListViewModel : ReactiveObject
{
    /// <inheritdoc />
    public RecordListViewModel(ICaseApplicationService caseAppService)
    {
        CaseAppService = Guard.Against.Null(caseAppService, nameof(caseAppService));
        var casesCache = new SourceCache<CaseItemViewModel, Guid>(@case => @case.Id);
        casesCache.Connect()
                  .Sort(SortExpressionComparer<CaseItemViewModel>.Descending(@case => @case.ReportedAt))
                  .Bind(out var cases)
                  .Subscribe();
        Cases = cases;
        this.WhenAnyValue(vm => vm.ReporterNo)
            .Where(no => no.IsNotNullOrEmpty())
            .DistinctUntilChanged()
            .SelectMany(no => CaseAppService.ListPagedCasesAsync(no, DateTimeOffset.Now.AddMonths(-3), DateTimeOffset.Now, 1, 10000))
            .Where(result => result.Data != null)
            .Select(result => result.Data!.Items)
            .Subscribe(casesList => casesCache.AddOrUpdateWith(casesList));
    }

    #region Properties

    public ICaseApplicationService CaseAppService { get; }

    public ReadOnlyObservableCollection<CaseItemViewModel> Cases { get; }

    private object? _currentCase;
    public object? CurrentCase
    {
        get => _currentCase;
        set => this.RaiseAndSetIfChanged(ref _currentCase, value);
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

}