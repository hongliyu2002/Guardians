using Guardians.Domain.Shared;
using JetBrains.Annotations;

namespace Guardians.Application.Contracts.States;

[PublicAPI]
[Serializable]
public sealed class CaseForCreationDto
{
    public SceneId SceneID { get; set; } = default!;

    public string ReporterNo { get; set; } = default!;

    public string ReporterName { get; set; } = default!;

    public string? ReporterMobile { get; set; }
}