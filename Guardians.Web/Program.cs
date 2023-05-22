using Fluxera.Extensions.Hosting;

namespace Guardians.Web;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        await ApplicationHost.RunAsync<GuardiansWebHost>(args);
    }
}