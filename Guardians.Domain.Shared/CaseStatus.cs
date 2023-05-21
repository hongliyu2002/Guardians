using Fluxera.Enumeration;
using JetBrains.Annotations;

namespace Guardians.Domain.Shared;

[PublicAPI]
public sealed class CaseStatus : Enumeration<CaseStatus>
{
    public static readonly CaseStatus Reviewing = new(0, "申报审核中");
    public static readonly CaseStatus Processing = new(1, "相关部分处理中");
    public static readonly CaseStatus Completed = new(2, "已完结");
    
    /// <inheritdoc />
    public CaseStatus(int value, string name)
        : base(value, name)
    {
    }
}