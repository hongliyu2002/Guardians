using System.IO.Compression;
using Fluxera.Extensions.Hosting;
using Fluxera.Extensions.Hosting.Modules;
using Fluxera.Extensions.Hosting.Modules.AspNetCore;
using Guardians.AspNetCore.Components.Server.Contributors;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace Guardians.AspNetCore.Components.Server;

[PublicAPI]
[DependsOn(typeof(AspNetCoreModule))]
public sealed class ComponentsServerModule : ConfigureServicesModule
{
    /// <inheritdoc />
    public override void PreConfigureServices(IServiceConfigurationContext context)
    {
        context.Services.AddEndpointRouteContributor<EndpointRouteContributor>();
    }

    /// <inheritdoc />
    public override void ConfigureServices(IServiceConfigurationContext context)
    {
        context.Log("ConfigureCompressionProviderOptions", services => services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal));
        context.Log("AddResponseCompression", services => services.AddResponseCompression(options =>
                                                                                          {
                                                                                              options.EnableForHttps = false;
                                                                                              options.Providers.Add<GzipCompressionProvider>();
                                                                                          }));
        context.Log("AddRazorPages", services => services.AddRazorPages());
        context.Log("AddServerSideBlazor", services => services.AddServerSideBlazor());
    }
}