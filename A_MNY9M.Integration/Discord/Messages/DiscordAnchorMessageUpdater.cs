using Microsoft.Extensions.Options;

using Discord;

using A_MNY9M.Integration.Discord.Abstractions;
using A_MNY9M.Integration.Discord.Options;

namespace A_MNY9M.Integration.Discord.AnchorMessages;

public class DiscordAnchorMessageUpdater(
    IOptions<MalenkieGuildOption> malenkieOptions,
    IDiscordV2ComponentsBuilder discordComponentsBuilder,
    IDiscordClientWrapper clientWrapper) : IDiscordAnchorMessageUpdater
{
    public async Task UpdateAsync()
    {
        await UpdateWelcomeMessageAsync();
    }

    private async Task UpdateWelcomeMessageAsync()
    {
        var components = await discordComponentsBuilder.BuildHubMessageComponentAsync();
        var channel = clientWrapper.MlkGuild.GetTextChannel(malenkieOptions.Value.AnchorMessages.ChannelsIn["HubDiscordId"]);

        if(channel is null)
        {
            return;
        }

        if(await channel.GetMessageAsync(malenkieOptions.Value.AnchorMessages.Ids["WelcomeDiscordId"]) is IUserMessage message)
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