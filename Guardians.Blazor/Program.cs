using Fluxera.Extensions.Hosting;

namespace Guardians.Blazor;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        await ApplicationHost.RunAsync<GuardiansBlazorHost>(args);
    }
}