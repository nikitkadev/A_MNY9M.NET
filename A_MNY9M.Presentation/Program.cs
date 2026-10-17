using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;

using A_MNY9M.Integration.DI;
using A_MNY9M.Infrastructure.DI;

namespace A_MNY9M.Presentation;

public class Program
{
    public static async Task Main()
    {
        Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        var builder = Host.CreateApplicationBuilder();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog();

        builder.Configuration.AddAmnyamDiscordConfiguration();
        builder.Services.AddAmnyamDiscordServices(builder.Configuration);
        builder.Services.AddAmnyamDatabaseServices(builder.Configuration);
       

        await builder.Build().RunAsync();
    }
}