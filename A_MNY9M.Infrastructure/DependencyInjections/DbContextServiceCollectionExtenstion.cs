using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using A_MNY9M.Infrastructure.Database;
using A_MNY9M.Infrastructure.Repositories;

namespace A_MNY9M.Infrastructure.DependencyInjection;

public static class DbContextServiceCollectionExtenstion
{
    public static IServiceCollection AddDbContextConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string connectionString = configuration["Database:LocalConnection"] ?? string.Empty;

        services.AddDbContext<AmnyamDbContext>(options =>
        {
            options.UseNpgsql(connectionString, options =>
            {
                options.CommandTimeout(300);
            });
        });

        return services;
    }

    public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
    {
        services.AddScoped<DiscordChannelRepository>();

        return services;
    }
}