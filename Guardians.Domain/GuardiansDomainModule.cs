using Fluxera.Extensions.Hosting;
using Fluxera.Extensions.Hosting.Modules;
using Fluxera.Extensions.Hosting.Modules.Configuration;
using Fluxera.Extensions.Hosting.Modules.Domain;
using Fluxera.Extensions.Hosting.Modules.Persistence;
using Guardians.Domain.Contributors;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Guardians.Domain;

[PublicAPI]
[DependsOn<DomainModule>]
[DependsOn<PersistenceModule>]
[DependsOn<ConfigurationModule>]
public sealed class GuardiansDomainModule : ConfigureServicesModule
{
    /// <inheritdoc />
    public override void ConfigureServices(IServiceConfigurationContext context)
    {
        context.Log("AddCaseRepository", services => services.TryAddScoped<ICaseRepository, CaseRepository>());
        context.Log("AddSceneRepository", services => services.TryAddScoped<ISceneRepository, SceneRepository>());
        context.Log("AddRepositoryContributor", services => services.AddRepositoryContributor<RepositoryContributor>());
    }
}