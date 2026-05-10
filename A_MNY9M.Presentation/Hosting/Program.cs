using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using Serilog;

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

        builder.Configuration.AddJsonFile(
            "Content/anchorMessages.json", 
            optional: true, 
            reloadOnChange: true);

        builder.Services.AddAppConfigurations(builder.Configuration);
        builder.Services.AddCoreServices();
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices();
        builder.Services.AddDiscordIntegrationServices();
        builder.Services.AddPresentationServices();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog();

        await builder.Build().RunAsync();
    }
}