using Fluxera.Guards;
using Guardians.Application.Contracts.States;
using ReactiveUI;

namespace Guardians.Blazor.ViewModels;

public class SceneItemViewModel : ReactiveObject
{
    public SceneItemViewModel(SceneDto scene)
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

    #endregion

    #region Load Scene

    public void UpdateWith(SceneDto scene)
    {
        Id = scene.ID.Value;
        Title = scene.Title;
    }

    #endregion

}