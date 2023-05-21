using Fluxera.Extensions.Hosting;
using Fluxera.Extensions.Hosting.Modules;
using Fluxera.Extensions.Hosting.Modules.Application;
using Fluxera.Extensions.Hosting.Modules.AutoMapper;
using Fluxera.Extensions.Hosting.Modules.Configuration;
using Guardians.Application.Contracts;
using Guardians.Application.Contributors;
using Guardians.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Guardians.Application;

[PublicAPI]
[DependsOn<GuardiansInfrastructureModule>]
[DependsOn<AutoMapperModule>]
[DependsOn<ApplicationModule>]
[DependsOn<ConfigurationModule>]
public sealed class GuardiansApplicationModule : ConfigureServicesModule
{
    /// <inheritdoc />
    public override void PreConfigureServices(IServiceConfigurationContext context)
    {
        context.Log("AddMappingProfileContributor", services => services.AddMappingProfileContributor<MappingProfileContributor>());
    }

    /// <inheritdoc />
    public override void ConfigureServices(IServiceConfigurationContext context)
    {
        context.Log("AddMediatR", services => services.AddMediatR());
        context.Log("AddCaseApplicationService", services => services.AddTransient<ICaseApplicationService, CaseApplicationService>());
        context.Log("AddSceneApplicationService", services => services.AddTransient<ISceneApplicationService, SceneApplicationService>());
    }
}