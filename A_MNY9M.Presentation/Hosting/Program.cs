using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;

using A_MNY9M.Presentation.Configuration;

namespace A_MNY9M.Presentation.Hosting;

public class Program
{
    public static async Task Main()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        var builder = Host.CreateApplicationBuilder();

        builder.Services.AddCoreServices();
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices();
        builder.Services.AddDiscordIntegrationServices();
        builder.Services.AddPresentationServices();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog();

        builder.Services.Configure<DiscordBotConfiguration>(builder.Configuration.GetSection("DiscordBotConfiguration"));


        await builder.Build().RunAsync();
    }
}