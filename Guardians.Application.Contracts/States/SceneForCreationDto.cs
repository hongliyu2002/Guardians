using JetBrains.Annotations;

namespace Guardians.Application.Contracts.States;

[PublicAPI]
public sealed class SceneForCreationDto
{
    public string Title { get; set; } = default!;
}