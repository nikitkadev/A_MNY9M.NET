using Serilog;
using Microsoft.Extensions.Hosting;
using Amnyam.Presentation.DI;

namespace Amnyam.Presentation;

class A_MNY9M_App
{
    public static async Task Main()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        IHost host = Host.CreateDefaultBuilder()
            .UseSerilog()
            .ConfigureServices((services) =>
            {
                services.AddDomainServices();
                services.AddApplicationServices();
                services.AddInfrastructureServices();
                services.AddPresentationServices();
            })
            .Build();

        await host.RunAsync();
    }
}