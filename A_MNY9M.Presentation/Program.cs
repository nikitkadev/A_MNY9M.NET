using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;

using A_MNY9M.Integration.Configurations.Extensions;

namespace A_MNY9M.Presentation;

public class Program
{
    public static async Task Main()
    {
        Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        var builder = Host.CreateApplicationBuilder();

        builder.Services.AddDiscordServices(builder.Configuration);
        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog();

        await builder.Build().RunAsync();
    }
}