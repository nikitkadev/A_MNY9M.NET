using Microsoft.Extensions.Options;

using Discord.WebSocket;

using A_MNY9M.Infrastructure.Repositories;
using A_MNY9M.Integration.Discord.Content.Models;
using Discord.Rest;

namespace A_MNY9M.Integration.Services;

public sealed class DiscordChannelService(
    IOptions<GuildConfigurationOption> guildOption,
    DiscordSocketClient discordSocketClient)
{
    public async Task<RestVoiceChannel?> CreateAutoVoiceChannelAsync(ulong creatorId, string channelName = "mlk_default")
    {
        var guild = discordSocketClient.GetGuild(guildOption.Value.DiscordId);

        var newChannel = await guild.CreateVoiceChannelAsync(
            name: channelName,
            prop =>
            {
                prop.Position = 1;
            });

        return newChannel;

    } 
}
