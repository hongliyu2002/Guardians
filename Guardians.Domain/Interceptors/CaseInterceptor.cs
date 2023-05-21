using System.Security.Claims;
using Fluxera.Extensions.Common;
using Fluxera.Extensions.Hosting.Modules.Domain.Interceptors;
using Fluxera.Extensions.Hosting.Modules.Principal;
using Fluxera.Guards;
using Guardians.Domain.Shared;
using JetBrains.Annotations;

namespace Guardians.Domain.Interceptors;

[UsedImplicitly]
public sealed class CaseInterceptor : AuditingInterceptorBase<Case, CaseId>
{
    private readonly IDateTimeOffsetProvider _dateTimeOffsetProvider;
    private readonly IPrincipalAccessor _principalAccessor;

    /// <inheritdoc />
    public CaseInterceptor(IDateTimeOffsetProvider dateTimeOffsetProvider, IPrincipalAccessor principalAccessor)
    {
        _dateTimeOffsetProvider = Guard.Against.Null(dateTimeOffsetProvider, nameof(dateTimeOffsetProvider));
        _principalAccessor = Guard.Against.Null(principalAccessor, nameof(principalAccessor));
    }

    /// <inheritdoc />
    protected override DateTimeOffset UtcNow => _dateTimeOffsetProvider.UtcNow;

    /// <inheritdoc />
    protected override ClaimsPrincipal Principal => _principalAccessor.User;
}