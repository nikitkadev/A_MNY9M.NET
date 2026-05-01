using Microsoft.Extensions.Options;

using Discord;
using Discord.WebSocket;

using A_MNY9M.Integration.Discord.Abstractions;
using A_MNY9M.Integration.Discord.Options;

namespace A_MNY9M.Integration.Discord.Client;

public class DiscordClientWrapper(
    IOptions<DiscordOption> discordOption,
    DiscordSocketClient discordSocketClient) : IDiscordClientWrapper
{
    public DiscordSocketClient DiscordSocketClient => discordSocketClient;
    public SocketGuild MlkGuild => discordSocketClient.GetGuild(discordOption.Value.MalenkieGuild.DiscordId);
    public async Task<Emote> GetApplicationEmoteAsync(ulong discordId)
    {
        return await discordSocketClient.GetApplicationEmoteAsync(discordId);
    }
}
