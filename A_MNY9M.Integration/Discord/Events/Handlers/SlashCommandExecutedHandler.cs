using Discord.WebSocket;

using A_MNY9M.Integration.Models.Params;
using A_MNY9M.Infrastructure.Models.Dtos;
using A_MNY9M.Integration.Discord.Commands.Content;
using A_MNY9M.Integration.Discord.Commands.Handlers;

namespace A_MNY9M.Integration.Discord.Events.Handlers;

public sealed class SlashCommandExecutedHandler(
    SetSourceVoiceChannelCommandHandler setSourceVoiceChannelCommandHandler,
    SetAutoLobbySettingsCommandHandler setAutoLobbySettingsCommandHandler)
{
    public async Task HandleAsync(SocketSlashCommand command)
    {
        await command.DeferAsync(ephemeral: true);

        switch (command.CommandName)
        {
            case DiscordCommandNames.SetSourceVoiceChannel:

                var channelOption = command.Data.Options
                    .FirstOrDefault(x => x.Name == DiscordCommandOptionNames.VoiceChannel);

                if (channelOption?.Value is not SocketVoiceChannel channel)
                {
                    await command.FollowupAsync(
                        "Не удалось получить голосовой канал.",
                        ephemeral: true);

                    return;
                }

                var setSourceChannelResult = await setSourceVoiceChannelCommandHandler.HandleAsync(
                    new SetSourceVoiceChannelParams(
                        DiscordId: channel.Id,
                        Name: channel.Name,
                        DiscordCategoryId: channel.CategoryId,
                        InitiatorDiscordId: command.User.Id));

                await command.FollowupAsync(setSourceChannelResult.ClientMessage);

                break;

            case DiscordCommandNames.SetAutoVoiceChannelSettings:

                var lobbyName = command.Data.Options.FirstOrDefault(
                    x => x.Name == DiscordCommandOptionNames.AutoVoiceChannelDefaultName)?.Value;

                if (lobbyName is null)
                {
                    await command.FollowupAsync(
                        "Не удалось получить голосовой канал",
                        ephemeral: true);

                    return;
                }

                int? userLimit = command.Data.Options.FirstOrDefault(
                    x => x.Name == DiscordCommandOptionNames.UserLimit)
                    ?.Value is long value
                        ? checked((int)value)
                        : null;

                string? region = command.Data.Options.FirstOrDefault(
                    x => x.Name == DiscordCommandOptionNames.Region)
                    ?.Value as string;

                var setAutoLobbySettingsResult = await setAutoLobbySettingsCommandHandler.HandleAsync(
                    command.User.Id,
                    command.User.Mention,
                    new AutoVoiceChannelSettingsDto(
                        Name: (string)lobbyName,
                        UserLimit: userLimit,
                        Region: region));

                await command.FollowupAsync(setAutoLobbySettingsResult.ClientMessage);

                break;

            default:

                await command.FollowupAsync("Команда пока что не реализована.");

                break;
        }
    }
}
