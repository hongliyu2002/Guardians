using JetBrains.Annotations;

namespace Guardians.Application.Contracts.States;

[PublicAPI]
[Serializable]
public sealed class CaseForUpdateDto
{
    public string? Description { get; set; }

    public string? Address { get; set; }

    public string? PhotoUrl { get; set; }
}