using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;

using A_MNY9M.Presentation.ServiceCollectionExtensions;

namespace A_MNY9M.Presentation;

public class Program
{
    public static async Task Main()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        var builder = Host.CreateApplicationBuilder();

        builder.Services.AddDiscordServices();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog();

        await builder.Build().RunAsync();
    }
}