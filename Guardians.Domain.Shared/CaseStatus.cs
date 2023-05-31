using JetBrains.Annotations;

namespace Guardians.Domain.Shared;

[PublicAPI]
public enum CaseStatus
{
    Reviewing = 0,
    Processing = 1,
    Completed = 2
}