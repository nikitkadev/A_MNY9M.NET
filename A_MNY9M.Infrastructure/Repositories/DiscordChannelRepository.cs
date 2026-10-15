using Microsoft.EntityFrameworkCore;
using A_MNY9M.Infrastructure.Database.Entities.Channels;
using A_MNY9M.Infrastructure.Database;

namespace A_MNY9M.Infrastructure.Repositories;

public sealed class DiscordChannelRepository(AmnyamDbContext dbContext)
{
    public async Task RemoveAutoVoiceChannelAsync(ulong discordId)
    {
        var channel = await dbContext
            .Set<AutoVoiceChannelDbEntity>()
            .FirstOrDefaultAsync(x => x.DiscordId == discordId);

        if (channel is null)
        {
            return;
        }

        dbContext.Set<AutoVoiceChannelDbEntity>().Remove(channel);

        await dbContext.SaveChangesAsync();
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
