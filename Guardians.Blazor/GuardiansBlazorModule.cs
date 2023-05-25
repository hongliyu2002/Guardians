using Fluxera.Extensions.Hosting;
using Fluxera.Extensions.Hosting.Modules;
using Fluxera.Extensions.Hosting.Modules.AspNetCore;
using Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks;
using Guardians.AspNetCore.Components.Server;
using Guardians.Blazor.ViewModels;
using Guardians.HttpClient;
using JetBrains.Annotations;
using MudBlazor.Services;

namespace Guardians.Blazor;

[PublicAPI]
[DependsOn<ComponentsServerModule>]
[DependsOn<HealthChecksEndpointsModule>]
[DependsOn<GuardiansHttpClientModule>]
public sealed class GuardiansBlazorModule : ConfigureApplicationModule
{
    /// <inheritdoc />
    public override void ConfigureServices(IServiceConfigurationContext context)
    {
        context.Log("AddMudServices", services => services.AddMudServices());
        context.Log("AddReportViewModel", services => services.AddScoped<ReportViewModel>());
    }

    /// <inheritdoc />
    public override void Configure(IApplicationInitializationContext context)
    {
        if (context.Environment.IsDevelopment())
        {
            context.UseDeveloperExceptionPage();
        }
        else
        {
            context.UseExceptionHandler("/error");
            context.UseHsts();
        }
        // context.UseHttpsRedirection();
        context.UseStaticFiles();
        context.UseRouting();
        context.UseEndpoints();
    }
}