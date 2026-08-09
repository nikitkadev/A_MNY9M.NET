using Microsoft.Extensions.Options;

using Discord.WebSocket;

using A_MNY9M.Integration.Discord.Options;
using A_MNY9M.Integration.Discord.Components.V2;

namespace A_MNY9M.Integration.Discord.Services;

public class DiscordTextMessageSender(
    IOptions<DiscordConfiguration> discordConfigurationOption,
    DiscordSocketClient discordSocketClient)
{
    public async Task SendRuleTextMessageAsync()
    {
        var dotEmote = await discordSocketClient.GetApplicationEmoteAsync(discordConfigurationOption.Value.Emotes.RuleDot);

        if (discordSocketClient.GetChannel(discordConfigurationOption.Value.Channels.Hub) is not SocketTextChannel targetChannel)
        {
            return;
        }

        await targetChannel.SendMessageAsync(components: RulesV2ComponentBuilder.Build(dotEmote));
    }
}