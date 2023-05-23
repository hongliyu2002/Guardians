using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Guardians.Blazor;

public partial class MainLayout
{
    [Inject]
    private IDialogService DialogService { get; set; } = default!;
}