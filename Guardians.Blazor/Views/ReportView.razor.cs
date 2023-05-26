using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using Fluxera.Utilities.Extensions;
using Guardians.Blazor.Models;
using Guardians.Blazor.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using MudBlazor;
using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Blazor;

namespace Guardians.Blazor.Views;

public partial class ReportView : ReactiveInjectableComponentBase<ReportViewModel>
{
    private IDisposable? _viewModelChangedSubscription;
    private IDisposable? _showSubmitCaseResultHandler;

    [Inject]
    private ISnackbar Snackbar { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Activate();
        ParseKnight();
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
        _showSubmitCaseResultHandler = ViewModel.ShowSubmitCaseResultInteraction.RegisterHandler(ShowSubmitCaseResult);
    }

    private void Deactivate()
    {
        _viewModelChangedSubscription?.Dispose();
        _showSubmitCaseResultHandler?.Dispose();
    }

    protected void ParseKnight()
    {
        var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
        var query = QueryHelpers.ParseQuery(uri.Query);
        var paramExists = query.TryGetValue("param", out var encryptedParam);
        if (!paramExists || encryptedParam.IsNullOrEmpty())
        {
            return;
        }
        var decryptedContent = Encryptor.DecryptData(encryptedParam[0]!, Encryptor.DailyPublicKeyBase64, Encoding.UTF8);
        var knightInfo = JsonConvert.DeserializeObject<KnightInfo>(decryptedContent);
        if (ViewModel != null)
        {
            if (knightInfo != null)
            {
                ViewModel.ReporterNo = knightInfo.KnightOid;
                ViewModel.ReporterName = knightInfo.KnightName;
                ViewModel.ReporterMobile = knightInfo.KnightMobile;
            }
            ViewModel.SearchTerm = "All";
        }
    }

    private void ShowSubmitCaseResult(InteractionContext<(string Message, bool Success), Unit> interaction)
    {
        // await DialogService.ShowMessageBox("提交结果", interaction.Input, "确定");
        Snackbar.Clear();
        Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
        Snackbar.Add(interaction.Input.Message, interaction.Input.Success ? Severity.Normal : Severity.Warning);
        interaction.SetOutput(Unit.Default);
    }
}