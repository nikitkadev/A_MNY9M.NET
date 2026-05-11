using Microsoft.Extensions.Options;

using Discord;

using A_MNY9M.Integration.Discord.Abstractions;
using A_MNY9M.Integration.Discord.Options;

namespace A_MNY9M.Integration.Discord.AnchorMessages;

public class DiscordAnchorMessageUpdater(
    IOptions<DiscordOption> discordAppOptions,
    IDiscordComponentsBuilder discordComponentsBuilder,
    IDiscordClientWrapper clientWrapper) : IDiscordAnchorMessageUpdater
{
    public async Task UpdateAsync()
    {
        await UpdateWelcomeMessageAsync();
    }

    private async Task UpdateWelcomeMessageAsync()
    {
        var components = await discordComponentsBuilder.BuildWelcomeMessageComponentsAsync();
        var channel = clientWrapper.MlkGuild.GetTextChannel(discordAppOptions.Value.AnchorChannels.HubDiscordId);

        if(channel is null)
        {
            return;
        }

        if(await channel.GetMessageAsync(discordAppOptions.Value.AnchorMessages.WelcomeDiscordId) is IUserMessage message)
        {
            await message.ModifyAsync(
                async message =>
                {
                    message.Components = components;
                });
        }
        else
        {
            await channel.SendMessageAsync(
                components: components);
        }
    }
}