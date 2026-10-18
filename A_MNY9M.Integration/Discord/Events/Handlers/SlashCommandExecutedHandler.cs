using Microsoft.Extensions.Logging;

using Discord.WebSocket;

using A_MNY9M.Integration.Constants;
using A_MNY9M.Integration.Models.Params;
using A_MNY9M.Infrastructure.Models.Dtos;
using A_MNY9M.Integration.Discord.Commands.Handlers;

namespace A_MNY9M.Integration.Discord.Events.Handlers;

public sealed class SlashCommandExecutedHandler(
    ILogger<SlashCommandExecutedHandler> logger,
    SetSourceVoiceChannelCommandHandler setSourceVoiceChannelCommandHandler,
    SetAutoLobbySettingsCommandHandler setAutoLobbySettingsCommandHandler)
{
    public async Task HandleAsync(SocketSlashCommand command)
    {
        try
        {
            await command.DeferAsync(ephemeral: true);

            switch (command.CommandName)
            {
                case DiscordCommandNames.SetSourceVoiceChannel:
                    {
                        var channelOption = command.Data.Options
                            .FirstOrDefault(
                                x => x.Name == DiscordCommandOptionNames.VoiceChannel);

                        if (channelOption?.Value is not SocketVoiceChannel channel)
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

                        await command.FollowupAsync(
                            result.ClientMessage,
                            ephemeral: true);

                        break;
                    }

                case DiscordCommandNames.SetAutoVoiceChannelSettings:
                    {
                        var lobbyName = command.Data.Options
                            .FirstOrDefault(
                                x => x.Name == DiscordCommandOptionNames.AutoVoiceChannelDefaultName)
                            ?.Value as string;

                        if (lobbyName is null)
                        {
                            await command.FollowupAsync(
                                "Не удалось получить имя личной комнаты.",
                                ephemeral: true);

                            return;
                        }

                        int? userLimit = command.Data.Options
                            .FirstOrDefault(
                                x => x.Name == DiscordCommandOptionNames.UserLimit)
                            ?.Value is long value
                                ? checked((int)value)
                                : null;

                        string? region = command.Data.Options
                            .FirstOrDefault(
                                x => x.Name == DiscordCommandOptionNames.Region)
                            ?.Value as string;

                        var result = await setAutoLobbySettingsCommandHandler.HandleAsync(
                            command.User.Id,
                            command.User.Mention,
                            new AutoVoiceChannelSettingsDto(
                                Name: lobbyName,
                                UserLimit: userLimit,
                                Region: region));

                        await command.FollowupAsync(
                            result.ClientMessage,
                            ephemeral: true);

                        break;
                    }

                default:
                    {
                        await command.FollowupAsync(
                            "Команда пока что не реализована.",
                            ephemeral: true);

                        break;
                    }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ошибка обработки команды {CommandName} от пользователя {UserId}. InteractionId: {InteractionId}.",
                command.CommandName,
                command.User.Id,
                command.Id);
        }
    }
}