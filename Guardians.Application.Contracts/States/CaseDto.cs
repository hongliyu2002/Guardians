using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
using Guardians.Domain.Shared;
using JetBrains.Annotations;

namespace Guardians.Application.Contracts.States;

[PublicAPI]
public sealed class CaseDto : EntityDto<CaseId>
{
    public SceneId SceneID { get; set; } = default!;

    public string SceneTitle { get; set; } = default!;

    public string? Description { get; set; }

    public string? Address { get; set; }

    public string? PhotoUrl { get; set; }

    public DateTimeOffset ReportedAt { get; set; }

    public string ReporterNo { get; set; } = default!;

    public string ReporterName { get; set; } = default!;

    public string? ReporterMobile { get; set; }

    public int StatusCode { get; set; }

    public string StatusName { get; set; } = default!;
}