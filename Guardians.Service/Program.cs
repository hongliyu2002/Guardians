using Fluxera.Extensions.Hosting;

namespace Guardians.Service;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        await ApplicationHost.RunAsync<GuardiansServiceHost>(args);
    }
}