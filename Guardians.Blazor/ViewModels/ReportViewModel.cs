using ReactiveUI;

namespace Guardians.Blazor.ViewModels;

public class ReportViewModel : ReactiveObject
{
    /// <inheritdoc />
    public ReportViewModel()
    {
    }

    private string _errorInfo = string.Empty;
    public string ErrorInfo
    {
        get => _errorInfo;
        set => this.RaiseAndSetIfChanged(ref _errorInfo, value);
    }
}
