using Discord.WebSocket;

using A_MNY9M.Integration.Models.Params;
using A_MNY9M.Integration.Discord.Commands.Content;
using A_MNY9M.Integration.Discord.Commands.Handlers;

namespace A_MNY9M.Integration.Discord.Events.Handlers;

public sealed class SlashCommandExecutedHandler(
    SetSourceVoiceChannelCommandHandler setSourceVoiceChannelCommandHandler)
{
    public async Task HandleAsync(SocketSlashCommand command)
    {
        await command.DeferAsync(ephemeral: true);

        switch (command.CommandName)
        {
            case DiscordCommandNames.SetSourceVoiceChannel:

                var channelOption = command.Data.Options
                    .FirstOrDefault(x => x.Name == DiscordCommandOptionNames.VoiceChannel);

                if(channelOption?.Value is not SocketVoiceChannel channel)
                {
                    await command.FollowupAsync(
                        "Не удалось получить голосовой канал.",
                        ephemeral: true);

                    return;
                }

                var result = await setSourceVoiceChannelCommandHandler.HandleAsync(
                    new SetSourceVoiceChannelParams(
                        DiscordId: channel.Id,
                        Name: channel.Name,
                        DiscordCategoryId: channel.CategoryId,
                        InitiatorDiscordId: command.User.Id));

                await command.FollowupAsync(result.ClientMessage);

                break;

            default:
                break;
        }
    }
}
