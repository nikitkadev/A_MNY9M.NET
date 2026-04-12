using A_MNY9M._1_Core.Interfaces;
using A_MNY9M._3_Infrastructure.Database.EF;
using A_MNY9M.Core.Entities.Configurations;
using Microsoft.EntityFrameworkCore;

namespace A_MNY9M._3_Infrastructure.Implementations.Repositiory;

public class RoomSettingsRepository(AmnyamDbContext dbContext) : IRoomSettingsRepository
{
    public async Task UpsertRoomSettingsAsync(VoiceChannelConfiguration roomSettings, CancellationToken token = default)
    {
        var settings = dbContext.RoomSettings
            .FirstOrDefault(rs => rs.GuildMemberDiscordId == roomSettings.GuildMemberDiscordId);

        if (settings is not null)
        {
            if(roomSettings.VoiceRoomName is not null)
                settings.VoiceRoomName = roomSettings.VoiceRoomName;

            if(roomSettings.MembersLimit is not null)
                settings.MembersLimit = roomSettings.MembersLimit;

            if(roomSettings.Region is not null)
                settings.Region = roomSettings.Region;

            return;
        }

        await dbContext.RoomSettings.AddAsync(roomSettings, token);
        await dbContext.SaveChangesAsync(token);
    }

    public async Task<VoiceChannelConfiguration> GetRoomSettingsByGuildMemberDiscordIdAsync(ulong guildMemberDiscordId, CancellationToken token = default)
    {
        var settings = await dbContext.RoomSettings.FindAsync([guildMemberDiscordId], cancellationToken: token);

        return settings ?? new VoiceChannelConfiguration() { GuildMemberDiscordId = guildMemberDiscordId};
    }

    public async Task RemoveRoomSettingsByGuildMemberDiscordIdAsync(ulong guildMemberDiscordId, CancellationToken token = default)
    {
        var settings = await dbContext.RoomSettings
            .FirstOrDefaultAsync(rs => rs.GuildMemberDiscordId == guildMemberDiscordId, cancellationToken: token) ?? throw new ArgumentNullException($"Настроек комнаты для участника с DiscordId {guildMemberDiscordId} не найдены");

        dbContext.RoomSettings.Remove(settings);

        await dbContext.SaveChangesAsync(token);
    }
}
