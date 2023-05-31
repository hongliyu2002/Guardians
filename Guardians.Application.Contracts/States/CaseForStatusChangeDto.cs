using Guardians.Domain.Shared;
using JetBrains.Annotations;

namespace Guardians.Application.Contracts.States;

[PublicAPI]
[Serializable]
public sealed class CaseForStatusChangeDto
{
    public CaseStatus Status { get; set; } = default!;
}