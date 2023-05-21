using System.Net;
using FluentValidation;
using Fluxera.Extensions.Hosting;
using Fluxera.Extensions.Hosting.Modules;
using Fluxera.Extensions.Hosting.Modules.AspNetCore;
using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi;
using JetBrains.Annotations;
using ProblemDetailsOptions = MadEyeMatt.AspNetCore.ProblemDetails.ProblemDetailsOptions;

namespace Guardians.Service;

[PublicAPI]
public sealed class GuardiansServiceModule : ConfigureApplicationModule
{
    /// <inheritdoc />
    public override void ConfigureServices(IServiceConfigurationContext context)
    {
        context.Services.Configure<ProblemDetailsOptions>(options =>
                                                          {
                                                              options.MapStatusCode<ValidationException>(HttpStatusCode.BadRequest);
                                                          });
    }

    /// <inheritdoc />
    public override void Configure(IApplicationInitializationContext context)
    {
        if (context.Environment.IsDevelopment())
        {
            context.UseSwaggerUI();
        }
        else
        {
            context.UseHsts();
        }
        context.UseHttpsRedirection();
        context.UseRouting();
        context.UseEndpoints();
    }
}