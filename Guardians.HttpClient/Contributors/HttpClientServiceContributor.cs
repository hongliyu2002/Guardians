using Fluxera.Extensions.Hosting;
using Fluxera.Extensions.Hosting.Modules.HttpClient;
using Fluxera.Extensions.Http;
using Guardians.Application.Contracts;
using Guardians.HttpClient.Services;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Guardians.HttpClient.Contributors;

[UsedImplicitly]
internal sealed class HttpClientServiceContributor : IHttpClientServiceContributor
{
    /// <inheritdoc />
    public IEnumerable<IHttpClientBuilder> AddNamedHttpClientServices(IServiceConfigurationContext context)
    {
        yield return context.Services.AddHttpClientService<ICaseApplicationService, CaseApplicationServiceClient>("Case", (ctx, _) => new CaseApplicationServiceClient(ctx.Name, ctx.HttpClient, ctx.Options));
        yield return context.Services.AddHttpClientService<ISceneApplicationService, SceneApplicationServiceClient>("Scene", (ctx, _) => new SceneApplicationServiceClient(ctx.Name, ctx.HttpClient, ctx.Options));
    }
}