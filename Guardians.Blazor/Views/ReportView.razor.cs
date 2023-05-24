using System.Reactive.Linq;
using Guardians.Blazor.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using ReactiveUI;
using ReactiveUI.Blazor;

namespace Guardians.Blazor.Views;

public partial class ReportView : ReactiveInjectableComponentBase<ReportViewModel>
{
    private IDisposable? _viewModelChangedSubscription;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Activate();
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        Deactivate();
        base.Dispose(disposing);
    }

    private void Activate()
    {
        if (ViewModel != null)
        {
            _viewModelChangedSubscription = this.WhenAnyObservable(v => v.ViewModel!.Changed)
                                                .Throttle(TimeSpan.FromMilliseconds(100))
                                                .Subscribe(_ => InvokeAsync(StateHasChanged));
        }
    }

    private void Deactivate()
    {
        _viewModelChangedSubscription?.Dispose();
    }

    [Inject]
    private IDialogService DialogService { get; set; } = default!;
}