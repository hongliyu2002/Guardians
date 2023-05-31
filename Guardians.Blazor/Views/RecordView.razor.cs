using System.Reactive.Linq;
using Fluxera.Utilities.Extensions;
using Guardians.Blazor.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using ReactiveUI;
using ReactiveUI.Blazor;

namespace Guardians.Blazor.Views;

public partial class RecordView : ReactiveInjectableComponentBase<RecordViewModel>
{
    private IDisposable? _viewModelChangedSubscription;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Activate();
        ParseCaseId();
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        Deactivate();
        base.Dispose(disposing);
    }

    private void Activate()
    {
        if (ViewModel == null)
        {
            return;
        }
        _viewModelChangedSubscription = this.WhenAnyObservable(v => v.ViewModel!.Changed)
                                            .Throttle(TimeSpan.FromMilliseconds(100))
                                            .Subscribe(_ => InvokeAsync(StateHasChanged));
    }

    private void Deactivate()
    {
        _viewModelChangedSubscription?.Dispose();
    }

    protected void ParseCaseId()
    {
        var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
        var query = QueryHelpers.ParseQuery(uri.Query);
        var idExists = query.TryGetValue("id", out var id);
        if (!idExists || id.IsNullOrEmpty())
        {
            return;
        }
        if (Guid.TryParse(id[0]!, out var caseId))
        {
            if (ViewModel != null)
            {
                ViewModel.CaseId = caseId;
            }
        }
    }
}