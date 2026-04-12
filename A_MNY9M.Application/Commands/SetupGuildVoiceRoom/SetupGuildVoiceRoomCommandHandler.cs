using Microsoft.Extensions.Logging;

using MediatR;

using A_MNY9M._1_Core.Results;
using A_MNY9M._1_Core.Enums;
using A_MNY9M._1_Core.Exceptions;
using A_MNY9M._1_Core.Interfaces;
using A_MNY9M._1_Core.Entities;

namespace A_MNY9M._2_Application.Commands.SetupGuildVoiceRoom;

public class SetupGuildVoiceRoomCommandHandler(
    ILogger<SetupGuildVoiceRoomCommandHandler> logger,
    IRoomSettingsRepository settingsRepository) : IRequestHandler<SetupGuildVoiceRoomCommand, BaseResult>
{
    public async Task<BaseResult> Handle(SetupGuildVoiceRoomCommand request, CancellationToken token)
    {
        try
        {
            if (request.MembersLimit < 0 || request.MembersLimit > 100)
                request.MembersLimit = 0;

            if (request?.RoomName?.Length > 15)
                request.RoomName = request.RoomName[..14];

            await settingsRepository.UpsertRoomSettingsAsync(
                new RoomSettings()
                {
                    GuildMemberDiscordId = request!.GuildMemberDiscordId,
                    VoiceRoomName = request.RoomName,
                    MembersLimit = request.MembersLimit,
                    Region = request.Region,
                }, token);

            logger.LogInformation(
                "Настройки успешно заданы для участника с DiscordId {GuildMemberDiscordId}",
                request.GuildMemberDiscordId);

            return BaseResult.Success(
                $"Параметры создаваемой комнаты успешно настроены!");
        }
        catch(GuildMemberNotFoundException ex)
        {
            logger.LogError(ex, 
                "Попытка изменить имя комнаты для несуществующего участника {GuildMemberDiscordId}",
                ex.GuildMemberDiscordId);

            return BaseResult.Fail(
                "Участник сервера не найден",
                new Error(
                    ErrorCodes.NOT_FOUND,
                    ex.Message));
        }
    }
}