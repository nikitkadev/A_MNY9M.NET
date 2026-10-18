using Microsoft.Extensions.Options;

using Discord.Rest;
using Discord.WebSocket;

using A_MNY9M.Infrastructure.Models.Dtos;
using A_MNY9M.Integration.Discord.Content.Models;

namespace A_MNY9M.Integration.Services;

public sealed class DiscordChannelService(
    IOptions<GuildConfigurationOption> guildOption,
    DiscordSocketClient discordSocketClient)
{
    public async Task<RestVoiceChannel?> CreateAutoVoiceChannelAsync(
        ulong creatorId,
        ulong? categoryId,
        AutoVoiceChannelSettingsDto settings)
    {
        var guild = discordSocketClient.GetGuild(guildOption.Value.TestDiscordId);

        var newChannel = await guild.CreateVoiceChannelAsync(
            name: settings.Name,
            prop =>
            {
                prop.Position = 1;

                if (categoryId is not null) prop.CategoryId = categoryId;
                if (!string.IsNullOrWhiteSpace(settings.Region)) prop.RTCRegion = settings.Region;
                if (settings.UserLimit is not null) prop.UserLimit = settings.UserLimit;

            });

        return newChannel;

    }
}
