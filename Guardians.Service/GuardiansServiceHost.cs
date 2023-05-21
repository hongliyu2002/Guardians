using System.Reflection;
using Fluxera.Extensions.Hosting;
using Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks;
using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
using Fluxera.Extensions.Hosting.Modules.Serilog;
using Fluxera.Extensions.Hosting.Plugins;
using OpenTelemetry.Logs;
using Serilog;
using Serilog.Extensions.Logging;

namespace Guardians.Service;

public class GuardiansServiceHost : WebApplicationHost<GuardiansServiceModule>
{
    /// <inheritdoc />
    protected override void ConfigureApplicationPlugins(IPluginConfigurationContext context)
    {
        context.AddPlugin<SerilogModule>();
        context.AddPlugin<HealthChecksEndpointsModule>();
    }

    /// <inheritdoc />
    protected override void ConfigureHostBuilder(IHostBuilder builder)
    {
        // Add user secrets configuration source.
        builder.ConfigureAppConfiguration(configurationBuilder => configurationBuilder.AddUserSecrets(Assembly.GetExecutingAssembly()));
        // Add OpenTelemetry logging.
        builder.AddOpenTelemetryLogging(options => options.AddConsoleExporter());
        // Add Serilog logging
        builder.AddSerilogLogging();
    }

    /// <inheritdoc />
    protected override ILoggerFactory CreateBootstrapperLoggerFactory(IConfiguration configuration)
    {
        var logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).Enrich.FromLogContext().CreateBootstrapLogger();
        var loggerFactory = new SerilogLoggerFactory(logger);
        return loggerFactory;
    }
}