using Fluxera.Extensions.Hosting;
using Fluxera.Extensions.Hosting.Modules.Persistence;
using Guardians.Domain.Interceptors;
using Guardians.Domain.Validations;
using JetBrains.Annotations;

namespace Guardians.Domain.Contributors;

[UsedImplicitly]
internal sealed class RepositoryContributor : RepositoryContributorBase
{
    /// <inheritdoc />
    public override void ConfigureAggregates(IRepositoryAggregatesBuilder builder, IServiceConfigurationContext context)
    {
        builder.UseFor<Case>();
        builder.UseFor<Scene>();
    }

    /// <inheritdoc />
    public override void ConfigureDomainEventHandlers(IDomainEventHandlersBuilder builder, IServiceConfigurationContext context)
    {
    }

    /// <inheritdoc />
    public override void ConfigureValidators(IValidatorsBuilder builder, IServiceConfigurationContext context)
    {
        builder.AddValidator<CaseValidator>();
        builder.AddValidator<SceneValidator>();
    }

    /// <inheritdoc />
    public override void ConfigureInterceptors(IInterceptorsBuilder builder, IServiceConfigurationContext context)
    {
        builder.AddInterceptor<CaseInterceptor>();
    }

    /// <inheritdoc />
    public override void ConfigureCaching(ICachingBuilder builder, IServiceConfigurationContext context)
    {
        builder.UseNoCaching().UseStandardFor<Scene>();
    }
}