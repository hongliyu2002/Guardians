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
    private IDialogService DialogService { get; set; } = default!;

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
        _showSubmitCaseResultHandler = ViewModel.ShowSubmitCaseResultInteraction.RegisterHandler(ShowSubmitCaseResultAsync);
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
        //测试环境公钥
        // MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCfS3rnByQ8QV3ayiwFZHP6aNxEDS92tU1W1VPQLSh7CsIlyx1GiuUjZeXbdMI7ylLBDWY7QM2hFNZDOQzHS8zzUx3f8Ayiy83BiJ+uM1JotprROP6QmP9VafapxdHjbuDQOZHclcbRxfwWME5dilKQSg2ygNoyHHg8F3Rt15OzOQIDAQAB
        // 生产环境公钥
        // MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC0FuzZpEBYTh1QKL+SM83qXYNLDx/V8u6QaDKG4i015hAhEMrb9ab1d9uStBF1jRwJMHPggWtIZSEio27ik2ONJuIpYSK5TiAiXjmWAXwc9r5VSTSwjK2IN0bw8N/Z0+JgObc91i4pApt9Dd5eqZOiN2qbDh1knDfnzA3OFsdyYwIDAQAB
        const string publicKeyBase64 = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCfS3rnByQ8QV3ayiwFZHP6aNxEDS92tU1W1VPQLSh7CsIlyx1GiuUjZeXbdMI7ylLBDWY7QM2hFNZDOQzHS8zzUx3f8Ayiy83BiJ+uM1JotprROP6QmP9VafapxdHjbuDQOZHclcbRxfwWME5dilKQSg2ygNoyHHg8F3Rt15OzOQIDAQAB";
        var decryptedContent = Encryptor.DecryptData(encryptedParam[0]!, publicKeyBase64, Encoding.UTF8);
        var knightInfo = JsonConvert.DeserializeObject<KnightInfo>(decryptedContent);
        if (ViewModel != null && knightInfo != null)
        {
            ViewModel.ReporterNo = knightInfo.KnightOid;
            ViewModel.ReporterName = knightInfo.KnightName;
            ViewModel.ReporterMobile = knightInfo.KnightMobile;
        }
    }

    private async Task ShowSubmitCaseResultAsync(InteractionContext<string, Unit> interaction)
    {
        await DialogService.ShowMessageBox("提交结果", interaction.Input, "确定");
        interaction.SetOutput(Unit.Default);
    }
}