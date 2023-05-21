using Fluxera.Extensions.Hosting;
using Fluxera.Extensions.Hosting.Modules;
using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi;
using Fluxera.Extensions.Hosting.Modules.Configuration;
using JetBrains.Annotations;

namespace Guardians.HttpApi;

[PublicAPI]
[DependsOn<HttpApiModule>]
[DependsOn<ConfigurationModule>]
public sealed class GuardiansHttpApiModule : ConfigureApplicationModule
{
}