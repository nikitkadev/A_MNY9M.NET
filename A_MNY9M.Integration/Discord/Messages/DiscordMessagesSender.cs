using Microsoft.Extensions.Options;

using Discord;

using A_MNY9M.Integration.Discord.Options;
using A_MNY9M.Integration.Discord.Abstractions;

namespace A_MNY9M.Integration.Discord.Messages;

public class DiscordMessagesSender(
    IOptions<MalenkieGuildOption> malenkieOptions,
    IDiscordClientWrapper clientWrapper) : IDiscordMessagesSender
{
    public async Task SendWelcomeMessageToNewMemberAsync(MessageComponent component)
    {
        var textChannel = clientWrapper.MlkGuild.GetTextChannel(malenkieOptions.Value.Channels.Text["Welcome"]);

        await textChannel.SendMessageAsync(
            components: component);
    }
}