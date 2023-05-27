using Fluxera.Guards;
using Guardians.Application.Contracts.States;
using ReactiveUI;

namespace Guardians.Blazor.ViewModels;

public class CaseItemViewModel : ReactiveObject
{
    public CaseItemViewModel(CaseDto @case)
    {
        Guard.Against.Null(@case, nameof(@case));
        UpdateWith(@case);
    }

    #region Properties

    private Guid _id;
    public Guid Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }

    private Guid _sceneId;
    public Guid SceneId
    {
        get => _sceneId;
        set => this.RaiseAndSetIfChanged(ref _sceneId, value);
    }

    private string _sceneTitle = string.Empty;
    public string SceneTitle
    {
        get => _sceneTitle;
        set => this.RaiseAndSetIfChanged(ref _sceneTitle, value);
    }

    private string? _description;
    public string? Description
    {
        get => _description;
        set => this.RaiseAndSetIfChanged(ref _description, value);
    }

    private string? _address;
    public string? Address
    {
        get => _address;
        set => this.RaiseAndSetIfChanged(ref _address, value);
    }

    private string? _photoUrl;
    public string? PhotoUrl
    {
        get => _photoUrl;
        set => this.RaiseAndSetIfChanged(ref _photoUrl, value);
    }

    private DateTimeOffset _reportedAt;
    public DateTimeOffset ReportedAt
    {
        get => _reportedAt;
        set => this.RaiseAndSetIfChanged(ref _reportedAt, value);
    }

    private string _reporterNo = string.Empty;
    public string ReporterNo
    {
        get => _reporterNo;
        set => this.RaiseAndSetIfChanged(ref _reporterNo, value);
    }

    private string _reporterName = string.Empty;
    public string ReporterName
    {
        get => _reporterName;
        set => this.RaiseAndSetIfChanged(ref _reporterName, value);
    }

    private string? _reporterMobile;
    public string? ReporterMobile
    {
        get => _reporterMobile;
        set => this.RaiseAndSetIfChanged(ref _reporterMobile, value);
    }

    private int _statusCode;
    public int StatusCode
    {
        get => _statusCode;
        set => this.RaiseAndSetIfChanged(ref _statusCode, value);
    }

    private string _statusName = string.Empty;
    public string StatusName
    {
        get => _statusName;
        set => this.RaiseAndSetIfChanged(ref _statusName, value);
    }

    #endregion

    #region Load Case

    public void UpdateWith(CaseDto @case)
    {
        Id = @case.ID.Value;
        SceneId = @case.SceneID.Value;
        SceneTitle = @case.SceneTitle;
        Description = @case.Description;
        Address = @case.Address;
        PhotoUrl = @case.PhotoUrl;
        ReportedAt = @case.ReportedAt;
        ReporterNo = @case.ReporterNo;
        ReporterName = @case.ReporterName;
        ReporterMobile = @case.ReporterMobile;
        StatusCode = @case.StatusCode;
        StatusName = @case.StatusName;
    }

    #endregion

}