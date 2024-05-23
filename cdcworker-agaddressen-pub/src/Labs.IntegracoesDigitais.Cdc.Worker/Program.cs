using Localiza.BuildingBlocks.Cdc.Infrastructure;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Localiza.BuildingBlocks.Cdc.Worker;

[ExcludeFromCodeCoverage]
internal static class Program
{
    private static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureCdc(builder =>
            {
                builder.Cdc = new CdcConfiguration();
                builder.Repository = RepositoryType.Sybase;
                builder.PathBase = "/cdcworker-agaddressen-pub";
            })
            .Build();

        try
        {
            await host.RunAsync();
        }
        catch (TaskCanceledException)
        {
            // let it stop!
        }
    }
}
