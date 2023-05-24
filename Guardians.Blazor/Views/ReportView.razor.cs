using System.Reactive;
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
    private IDisposable? _showSubmitCaseResultHandler;

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
        if (ViewModel == null)
        {
            return;
        }
        _viewModelChangedSubscription = this.WhenAnyObservable(v => v.ViewModel!.Changed)
                                            .Throttle(TimeSpan.FromMilliseconds(100))
                                            .Subscribe(_ => InvokeAsync(StateHasChanged));
        _showSubmitCaseResultHandler = ViewModel.ShowSubmitCaseResultInteraction.RegisterHandler(ShowSubmitCaseResultAsync);
    }

    private void Deactivate()
    {
        _viewModelChangedSubscription?.Dispose();
        _showSubmitCaseResultHandler?.Dispose();
    }

    [Inject]
    private IDialogService DialogService { get; set; } = default!;

    private async Task ShowSubmitCaseResultAsync(InteractionContext<string, Unit> interaction)
    {
        await DialogService.ShowMessageBox("提交结果", interaction.Input, "确定");
        interaction.SetOutput(Unit.Default);
    }
}