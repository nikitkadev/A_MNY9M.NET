using Microsoft.EntityFrameworkCore;
using A_MNY9M._1_Core.Interfaces;
using A_MNY9M._3_Infrastructure.Database.EF;
using A_MNY9M.Core.Entities;

namespace A_MNY9M._3_Infrastructure.Implementations.Repositiory;

public class GuildMemberMetricRepository(
    AmnyamDbContext dbContext) : IGuildMemberMetricRepository
{
    private async Task ChangeDbPropertyAsync(ulong guildMemberDiscordId, Action<MemberMetric> action)
    {
        var metric = await dbContext.GuildMemberMetrics.FindAsync(guildMemberDiscordId);

        if (metric is null)
        {
            metric = new MemberMetric()
            {
                MemberDiscordId = guildMemberDiscordId
            };

            await dbContext.GuildMemberMetrics.AddAsync(metric);
        }

        action(metric);

        await dbContext.SaveChangesAsync();
    }

    public async Task IncrementMessageSentCountAsync(ulong guildMemberDiscordId, int increment = 1)
    {
        await ChangeDbPropertyAsync(guildMemberDiscordId, member => member.MessageSentCount += increment);
    }
    public async Task IncrementReactionAddedCountAsync(ulong guildMemberDiscordId, int increment = 1)
    {
        await ChangeDbPropertyAsync(guildMemberDiscordId, member => member.ReactionAddedCount += increment);
    }
    public async Task IncrementCommandSentCountAsync(ulong guildMemberDiscordId, int increment = 1)
    {
        await ChangeDbPropertyAsync(guildMemberDiscordId, member => member.CommandsSentCount += increment);
    }
    public async Task IncrementStickerCountAsync(ulong guildMemberDiscordId, int increment = 1)
    {
        await ChangeDbPropertyAsync(guildMemberDiscordId, member => member.StickersSentCount += increment);
    }
    public async Task IncrementGifSentCountAsync(ulong guildMemberDiscordId, int increment = 1)
    {
        await ChangeDbPropertyAsync(guildMemberDiscordId, member => member.GifsSentCount += increment);
    }
    public async Task IncrementPngPictureSentCountAsync(ulong guildMemberDiscordId, int increment = 1)
    {
        await ChangeDbPropertyAsync(guildMemberDiscordId, member => member.PngPicturesSentCount += increment);
    }
    public async Task UpdateLastMessageDateAsync(ulong guildMemberDiscordId)
    {
        await ChangeDbPropertyAsync(guildMemberDiscordId, member => member.LastMessage = DateTimeOffset.UtcNow);
    }

    public async Task<MemberMetric> GetGuildMemberMetricAsync(ulong guildMemberDiscordId)
    {
        var memberMetric = await dbContext.GuildMemberMetrics.FirstOrDefaultAsync(metric => metric.MemberDiscordId == guildMemberDiscordId);

        if(memberMetric is null)
            return new MemberMetric()
            {
                MemberDiscordId = guildMemberDiscordId
            };

        return memberMetric;
    }
}