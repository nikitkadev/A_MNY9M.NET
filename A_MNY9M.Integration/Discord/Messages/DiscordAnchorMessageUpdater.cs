using Microsoft.Extensions.Options;

using Discord;

using A_MNY9M.Integration.Discord.Abstractions;
using A_MNY9M.Integration.Discord.Options;

namespace A_MNY9M.Integration.Discord.Messages;

public class DiscordAnchorMessageUpdater(
    IOptions<MalenkieGuildOption> malenkieOptions,
    IDiscordV2ComponentsBuilder discordComponentsBuilder,
    IDiscordClientWrapper clientWrapper) : IDiscordAnchorMessageUpdater
{
    public async Task UpdateAsync()
    {
        await UpdateWelcomeMessageAsync();
        await UpdateStatisticMessageAsync();
    }

    private async Task UpdateWelcomeMessageAsync()
    {
        var component = await discordComponentsBuilder.BuildHubMessageComponentAsync();
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
                    message.Components = component;
                });

            return;
        }

        await channel.SendMessageAsync(
            components: component);
    }

    private async Task UpdateStatisticMessageAsync()
    {
        var component = await discordComponentsBuilder.BuildStatisticMessageComponentAsync();
        var channel = clientWrapper.MlkGuild.GetTextChannel(malenkieOptions.Value.AnchorMessages.ChannelsIn["HubDiscordId"]);

        if(channel is null)
        {
            return;
        }

        if(await channel.GetMessageAsync(malenkieOptions.Value.AnchorMessages.Ids["Statistic"]) is IUserMessage message)
        {
            await message.ModifyAsync(
                async message =>
                {
                    message.Components = component;
                });

            return;
        }

        await channel.SendMessageAsync(
            components: component);
    }
}