using System.Reactive.Linq;
using System.Text;
using System.Text.Json;
using Fluxera.Utilities.Extensions;
using Guardians.Application.Contracts.Utils;
using Guardians.Blazor.Models;
using Guardians.Blazor.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using ReactiveUI;
using ReactiveUI.Blazor;

namespace Guardians.Blazor.Views;

public partial class RecordListView : ReactiveInjectableComponentBase<RecordListViewModel>
{
    private IDisposable? _viewModelChangedSubscription;

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
        _viewModelChangedSubscription = this.WhenAnyObservable(v => v.ViewModel!.Changed).Throttle(TimeSpan.FromMilliseconds(100)).Subscribe(_ => InvokeAsync(StateHasChanged));
    }

    private void Deactivate()
    {
        _viewModelChangedSubscription?.Dispose();
    }

    protected void ParseKnight()
    {
        var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
        var uriquery = uri.Query.Replace("+", "%2B");
        var query = QueryHelpers.ParseQuery(uriquery);
        var paramExists = query.TryGetValue("param", out var encryptedParam);
        if (!paramExists || encryptedParam.IsNullOrEmpty())
        {
            return;
        }
        var decryptedContent = Encryptor.DecryptData(encryptedParam.ToString(), Encryptor.DailyPublicKeyBase64, Encoding.UTF8);
        var knightInfo = JsonSerializer.Deserialize<KnightInfo>(decryptedContent);
        if (ViewModel != null)
        {
            if (knightInfo != null)
            {
                ViewModel.ReporterNo = knightInfo.KnightOid;
            }
        }
    }

    private void NavigateToDetail()
    {
        if (ViewModel is { CurrentCase: not null })
        {
            var currentCase = (CaseItemViewModel)ViewModel.CurrentCase;
            NavigationManager.NavigateTo($"/record?id={currentCase.Id}", false);
        }
    }
}