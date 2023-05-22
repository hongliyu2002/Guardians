using System.Net;
using Fluxera.Extensions.Hosting;
using Fluxera.Extensions.Hosting.Modules;
using Fluxera.Extensions.Hosting.Modules.AspNetCore;
using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi;
using Fluxera.Extensions.Validation;
using Guardians.Application;
using Guardians.HttpApi;
using Guardians.Infrastructure.Contexts;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ProblemDetailsOptions = MadEyeMatt.AspNetCore.ProblemDetails.ProblemDetailsOptions;

namespace Guardians.Service;

[PublicAPI]
[DependsOn<GuardiansHttpApiModule>]
[DependsOn<GuardiansApplicationModule>]
public sealed class GuardiansServiceModule : ConfigureApplicationModule
{
    /// <inheritdoc />
    public override void ConfigureServices(IServiceConfigurationContext context)
    {
        context.Services.Configure<ProblemDetailsOptions>(options =>
                                                          {
                                                              options.MapStatusCode<InvalidOperationException>(HttpStatusCode.MethodNotAllowed);
                                                              options.MapStatusCode<ValidationException>(HttpStatusCode.BadRequest, (httpContext, exception, httpStatusCode, problemDetailsFactory) =>
                                                                                                                                    {
                                                                                                                                        var modelState = new ModelStateDictionary();
                                                                                                                                        foreach (var error in exception.Errors)
                                                                                                                                        {
                                                                                                                                            foreach (var errorMessage in error.ErrorMessages)
                                                                                                                                            {
                                                                                                                                                modelState.AddModelError(error.PropertyName, errorMessage);
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                        return problemDetailsFactory.CreateValidationProblemDetails(httpContext, modelState, (int)httpStatusCode);
                                                                                                                                    });
                                                              options.MapStatusCode<Exception>(HttpStatusCode.InternalServerError);
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
            context.UseSwaggerUI();
            context.UseHsts();
        }
        context.UseProblemDetails();
        // context.UseHttpsRedirection();
        context.UseRouting();
        context.UseEndpoints();
        
        using var scope = context.ServiceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GuardiansDbContext>();
        dbContext.Database.Migrate();
    }
}