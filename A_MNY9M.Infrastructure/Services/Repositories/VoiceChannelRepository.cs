using Microsoft.EntityFrameworkCore;

using A_MNY9M.Infrastructure.Database;
using A_MNY9M.Infrastructure.Database.Entities.Channels;

namespace A_MNY9M.Infrastructure.Services.Repositories;

public sealed class VoiceChannelRepository(
    AmnyamDbContext dbContext)
{
    public async Task RemoveAutoVoiceChannelAsync(
        ulong discordId,
        CancellationToken cancellationToken)
    {
        var channel = await dbContext
            .Set<AutoVoiceChannelDbEntity>()
            .Where(x => x.DiscordId == discordId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (channel is null)
        {
            return;
        }

        dbContext
            .Set<AutoVoiceChannelDbEntity>()
            .Remove(channel);

        await dbContext.SaveChangesAsync(
            cancellationToken: cancellationToken);
    }

    public async Task RemoveSourceVoiceChannelAsync(
        ulong discordId,
        CancellationToken cancellationToken)
    {
        var channel = await dbContext
            .Set<SourceVoiceChannelDbEntity>()
            .Where(x => x.DiscordId == discordId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        
        if (channel is null)
        {
            return;
        }

        dbContext
            .Set<SourceVoiceChannelDbEntity>()
            .Remove(channel);

        await dbContext.SaveChangesAsync(
            cancellationToken: cancellationToken);

    }

    public async Task<bool> IsAutoVoiceChannelAsync(ulong discordId)
    {
        return await dbContext
            .Set<AutoVoiceChannelDbEntity>()
            .AnyAsync(x => x.DiscordId == discordId);
    }

    public async Task<bool> IsSourceVoiceChannelAsync(ulong discordId)
    {
        return await dbContext
            .Set<SourceVoiceChannelDbEntity>()
            .AnyAsync(x => x.DiscordId == discordId);
    }
}
