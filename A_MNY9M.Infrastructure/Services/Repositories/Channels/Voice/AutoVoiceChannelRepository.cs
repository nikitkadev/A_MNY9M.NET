using Microsoft.EntityFrameworkCore;

using A_MNY9M.Infrastructure.Database;
using A_MNY9M.Infrastructure.Models.Dtos;
using A_MNY9M.Infrastructure.Database.Entities.Channels;
using A_MNY9M.Infrastructure.Database.Entities.Settings;
using A_MNY9M.Core.Common.Result;

namespace A_MNY9M.Infrastructure.Services.Repositories.Channels.Voice;

public sealed class AutoVoiceChannelRepository(
    AmnyamDbContext dbContext)
{
    public async Task<Guid?> AddAutoVoiceChannelAsync(
        AutoVoiceChannelDbEntity autoVoiceChannelDbEntity,
        CancellationToken cancellationToken = default)
    {
        var exists = await dbContext
            .Set<AutoVoiceChannelDbEntity>()
            .AnyAsync(
                x => x.DiscordId == autoVoiceChannelDbEntity.DiscordId,
                cancellationToken: cancellationToken);

        if (exists) return null;

        await dbContext.Set<AutoVoiceChannelDbEntity>()
            .AddAsync(
                autoVoiceChannelDbEntity,
                cancellationToken: cancellationToken);

        await dbContext.SaveChangesAsync(
            cancellationToken: cancellationToken);

        return autoVoiceChannelDbEntity.Id;
    }

    public async Task RemoveAutoVoiceChannelAsync(
        ulong autoVoiceChannelDiscordId,
        CancellationToken cancellationToken = default)
    {
        var channel = await dbContext
            .Set<AutoVoiceChannelDbEntity>()
            .Where(x => x.DiscordId == autoVoiceChannelDiscordId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (channel is null) return;

        dbContext.Set<AutoVoiceChannelDbEntity>()
            .Remove(channel);

        await dbContext.SaveChangesAsync(
            cancellationToken: cancellationToken);
    }

    public async Task<bool> IsAutoVoiceChannelAsync(
        ulong autoVoiceChannelDiscordId,
        CancellationToken cancellationToken = default)
    {
        return await dbContext
            .Set<AutoVoiceChannelDbEntity>()
            .AnyAsync(
                x => x.DiscordId == autoVoiceChannelDiscordId,
                cancellationToken: cancellationToken);
    }

    public async Task<AutoVoiceChannelSettingsDto> GetAutoVoiceChannelSettingsAsync(
        ulong userDiscordId,
        CancellationToken cancellationToken = default)
    {
        var settings = await dbContext
            .Set<AutoVoiceChannelSettingsDbEntity>()
            .Where(x => x.UserDiscordId == userDiscordId)
            .Select(x => new AutoVoiceChannelSettingsDto(
                Name: x.DefaultName,
                UserLimit: x.UserLimit,
                Region: x.Region))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (settings is null)
        {
            return new AutoVoiceChannelSettingsDto(
                "mlk_autovoice",
                UserLimit: null,
                Region: null);
        }

        return settings;
    }

    public async Task<Result<Guid>> SaveAutoVoiceChannelSettingsAsync(
        AutoVoiceChannelSettingsDbEntity requestSettings,
        CancellationToken cancellationToken = default)
    {
        var settings = await dbContext
            .Set<AutoVoiceChannelSettingsDbEntity>()
            .Where(x => x.UserDiscordId == requestSettings.UserDiscordId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if(settings is not null)
        {
            settings.DefaultName = requestSettings.DefaultName;
            settings.UserLimit = requestSettings.UserLimit;
            settings.Region = requestSettings.Region;

            await dbContext.SaveChangesAsync(
                cancellationToken: cancellationToken);

            return Result<Guid>.Success(
                value: settings.Id,
                clientMessage: "Настройки были заменены на новые.");
        }

        await dbContext
            .Set<AutoVoiceChannelSettingsDbEntity>()
            .AddAsync(
                requestSettings, 
                cancellationToken: cancellationToken);

        await dbContext.SaveChangesAsync(
            cancellationToken: cancellationToken);

        return Result<Guid>.Success(
            value: requestSettings.Id,
            clientMessage: "Настройки для личной комнаты успешно установлены.");
    }
}
