using Fluxera.Guards;
using Guardians.Domain;
using ReactiveUI;

namespace Guardians.Blazor.ViewModels;

public class SceneItemViewModel : ReactiveObject
{
    public SceneItemViewModel(Scene scene)
    {
        Guard.Against.Null(scene, nameof(scene));
        UpdateWith(scene);
    }

    #region Properties

    private Guid _id;
    public Guid Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }

    private string _title = string.Empty;
    public string Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    private bool _isDeleted;
    public bool IsDeleted
    {
        get => _isDeleted;
        set => this.RaiseAndSetIfChanged(ref _isDeleted, value);
    }

    #endregion

    #region Load Scene

    public void UpdateWith(Scene scene)
    {
        Id = scene.ID.Value;
        Title = scene.Title;
        IsDeleted = scene.IsDeleted;
    }

    #endregion
}
