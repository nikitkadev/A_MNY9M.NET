using Discord.WebSocket;

using A_MNY9M.Integration.Discord.Abstractions;

namespace A_MNY9M.Integration.Discord.Client;

public class DiscordClientWrapper(
    DiscordSocketClient discordSocketClient) : IDiscordClientWrapper
{
    public DiscordSocketClient DiscordSocketClient => discordSocketClient;
}
