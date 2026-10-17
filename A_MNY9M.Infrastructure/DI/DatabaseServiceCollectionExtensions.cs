using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using A_MNY9M.Infrastructure.Database;
using A_MNY9M.Infrastructure.Options;
using A_MNY9M.Infrastructure.Services.Repositories;
using A_MNY9M.Infrastructure.Services.Repositories.Channels.Voice;

namespace A_MNY9M.Infrastructure.DI;

public static class DatabaseServiceCollectionExtensions
{
    public static IServiceCollection AddAmnyamDatabaseServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ConfigureDatabaseOptions(services, configuration);
        RegisterDbContext(services);
        RegisterRepositories(services);

        return services;
    }

    private static void ConfigureDatabaseOptions(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<DatabaseOption>()
            .Bind(configuration.GetSection(DatabaseOption.SectionName))
            .Validate(
                options => !string.IsNullOrWhiteSpace(options.LocalConnectionString),
                "Не задана строка подключения к базе.")
            .ValidateOnStart();
    }

    private static void RegisterDbContext(
        IServiceCollection services)
    {
        services.AddDbContext<AmnyamDbContext>((sp, optionsBuilder) =>
        {
            var databaseOptions = sp.GetRequiredService<IOptions<DatabaseOption>>().Value;

            optionsBuilder.UseNpgsql(
                databaseOptions.LocalConnectionString,
                options =>
                {
                    options.CommandTimeout(databaseOptions.CommandTimeoutInSeconds);
                });
        });
    }

    private static void RegisterRepositories(
        IServiceCollection services)
    {
        services.AddScoped<VoiceChannelRepository>();
        services.AddScoped<SourceVoiceChannelRepository>();
    }
}