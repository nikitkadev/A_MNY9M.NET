using Microsoft.EntityFrameworkCore;

using A_MNY9M.Infrastructure.Database;
using A_MNY9M.Infrastructure.Database.Entities.Channels;

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
}
