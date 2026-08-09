using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Discord.WebSocket;

using A_MNY9M.Integration.Discord.Components.V2;
using A_MNY9M.Integration.Configurations.Options;

namespace A_MNY9M.Integration.Discord.Services;

public class DiscordTextMessageSender(
    ILogger<DiscordTextMessageSender> logger,
    IOptions<DiscordConfiguration> discordConfigurationOption,
    DiscordSocketClient discordSocketClient)
{
    public async Task SendRuleTextMessageAsync()
    {
        var dotEmoteDiscordId = discordConfigurationOption.Value.Emotes.RuleDot;
        var targetChannelDiscordId = discordConfigurationOption.Value.Channels.Hub;

        var channel = discordSocketClient.GetChannel(targetChannelDiscordId);

        if (channel is not SocketTextChannel textChannel)
        {
            logger.LogWarning(
                "Канал с DiscordId {ChannelDiscordId} не является текстовым", 
                targetChannelDiscordId);

            return;
        }

        var dotEmote = await discordSocketClient.GetApplicationEmoteAsync(dotEmoteDiscordId);

        await textChannel.SendMessageAsync(components: RulesV2ComponentBuilder.Build(dotEmote));
    }
}