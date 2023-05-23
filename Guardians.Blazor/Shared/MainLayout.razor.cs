using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Guardians.Blazor;

public partial class MainLayout
{
    [Inject]
    private IDialogService DialogService { get; set; } = default!;

    private bool _drawerOpen = true;
    private Anchor _drawerAnchor;

    private void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }
    
    private void ToggleDrawerAnchor()
    {
        _drawerAnchor = _drawerAnchor == Anchor.Start ? Anchor.End : Anchor.Start;
    }
}