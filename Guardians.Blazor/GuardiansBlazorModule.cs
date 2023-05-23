﻿using Fluxera.Extensions.Hosting;
using Fluxera.Extensions.Hosting.Modules;
using Fluxera.Extensions.Hosting.Modules.AspNetCore;
using Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks;
using Guardians.AspNetCore.Components.Server;
using Guardians.Infrastructure.Contexts;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

namespace Guardians.Blazor;

[PublicAPI]
[DependsOn<ComponentsServerModule>]
[DependsOn<HealthChecksEndpointsModule>]
public sealed class GuardiansBlazorModule : ConfigureApplicationModule
{
    /// <inheritdoc />
    public override void ConfigureServices(IServiceConfigurationContext context)
    {
        context.Log("AddMudServices", services => services.AddMudServices());
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
            context.UseExceptionHandler("/Error");
            context.UseHsts();
        }
        // context.UseHttpsRedirection();
        context.UseStaticFiles();
        context.UseRouting();
        context.UseEndpoints();
        using var scope = context.ServiceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GuardiansDbContext>();
        dbContext.Database.Migrate();
    }
}