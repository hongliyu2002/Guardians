using Fluxera.Entity;
using Fluxera.Extensions.Hosting.Modules.Domain.Shared.Model;
using Guardians.Domain.Shared;
using JetBrains.Annotations;

namespace Guardians.Domain;

[PublicAPI]
public sealed class Case : AggregateRoot<Case, CaseId>, IAuditedObject, ISoftDeleteObject
{
    public SceneId SceneID { get; set; } = default!;
    
    public Scene Scene { get; set; } = default!;
    
    public string Title { get; set; } = default!;

    public string? Description { get; set; }
        
    public string? Address { get; set; }
    
    public string? PhotoUrl { get; set; }
    
    public DateTimeOffset ReportedAt { get; set; }
    
    public string ReporterNo { get; set; } = default!;
    
    public string ReporterName { get; set; } = default!;
    
    public string? ReporterMobile { get; set; }
    
    public CaseStatus Status { get; set; } = CaseStatus.Reviewing;

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? LastModifiedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? LastModifiedBy { get; set; }

    public string? DeletedBy { get; set; }
    
    public bool IsDeleted { get; set; }
}