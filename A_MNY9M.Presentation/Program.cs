using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;

using A_MNY9M.Integration.Extensions;
using A_MNY9M.Infrastructure.DependencyInjection;

namespace A_MNY9M.Presentation;

public class Program
{
    public static async Task Main()
    {
        Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        var builder = Host.CreateApplicationBuilder();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog();

        builder.Configuration.AddDiscordJsonSources();

        builder.Services.AddDiscordServices();
        builder.Services.AddHandlerServices();
        builder.Services.AddDbContextConfiguration(builder.Configuration);
        builder.Services.ConfigureIntegrationOptions(builder.Configuration);

        await builder.Build().RunAsync();
    }
}