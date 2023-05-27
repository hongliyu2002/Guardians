using System.Reactive.Linq;
using Fluxera.Guards;
using Fluxera.Utilities.Extensions;
using Guardians.Application.Contracts;
using Guardians.Domain.Shared;
using ReactiveUI;

namespace Guardians.Blazor.ViewModels;

public class RecordViewModel : ReactiveObject
{
    /// <inheritdoc />
    public RecordViewModel(ICaseApplicationService caseAppService)
    {
        CaseAppService = Guard.Against.Null(caseAppService, nameof(caseAppService));
        this.WhenAnyValue(vm => vm.CaseId)
            .Where(id => id.IsNotEmpty())
            .DistinctUntilChanged()
            .SelectMany(id => CaseAppService.GetCaseAsync(new CaseId(id)))
            .Where(result => result.Data != null)
            .Select(result => result.Data!)
            .Subscribe(caseDto => CurrentCase = new CaseItemViewModel(caseDto));
    }

    #region Properties

    public ICaseApplicationService CaseAppService { get; }

    private CaseItemViewModel? _currentCase;
    public CaseItemViewModel? CurrentCase
    {
        get => _currentCase;
        set => this.RaiseAndSetIfChanged(ref _currentCase, value);
    }

    private Guid _caseId = Guid.Empty;
    public Guid CaseId
    {
        get => _caseId;
        set => this.RaiseAndSetIfChanged(ref _caseId, value);
    }

    #endregion

}