using Microsoft.Extensions.Options;

using Discord.WebSocket;

using A_MNY9M.Integration.Discord.Content.Models;

namespace A_MNY9M.Integration.Services;

public sealed class GuildLogService(
    IOptions<GuildConfigurationOption> guildOption,
    DiscordSocketClient discordSocketClient)
{
    public async Task SendLogAsync(string message)
    {
        var guild = discordSocketClient.GetGuild(guildOption.Value.DiscordId);
        var channel = guild.GetTextChannel(0);

        await channel.SendMessageAsync(message);
    }
}
