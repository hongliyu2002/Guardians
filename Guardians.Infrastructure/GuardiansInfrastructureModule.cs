using Fluxera.Extensions.Hosting;
using Fluxera.Extensions.Hosting.Modules;
using Fluxera.Extensions.Hosting.Modules.Configuration;
using Fluxera.Extensions.Hosting.Modules.Persistence.EntityFrameworkCore;
using Guardians.Domain;
using JetBrains.Annotations;

namespace Guardians.Infrastructure;

[PublicAPI]
[DependsOn<GuardiansDomainModule>]
[DependsOn<EntityFrameworkCorePersistenceModule>]
[DependsOn<ConfigurationModule>]
public sealed class GuardiansInfrastructureModule : ConfigureServicesModule
{
}