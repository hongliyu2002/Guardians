using Fluxera.Extensions.Hosting;
using Fluxera.Extensions.Hosting.Modules;
using Fluxera.Extensions.Hosting.Modules.Configuration;
using Fluxera.Extensions.Hosting.Modules.HttpClient;
using Guardians.HttpClient.Contributors;
using JetBrains.Annotations;

namespace Guardians.HttpClient;

[PublicAPI]
[DependsOn<HttpClientModule>]
[DependsOn<ConfigurationModule>]
public sealed class GuardiansHttpClientModule : ConfigureApplicationModule
{
    /// <inheritdoc />
    public override void PreConfigureServices(IServiceConfigurationContext context)
    {
        context.Services.AddHttpClientServiceContributor<HttpClientServiceContributor>();
    }
}