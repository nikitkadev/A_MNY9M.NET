using Microsoft.Extensions.Logging;

using MediatR;

using A_MNY9M._1_Core.Enums;
using A_MNY9M._1_Core.Interfaces;

using A_MNY9M._1_Core.Results;

namespace A_MNY9M._2_Application.Commands.RemoveVoiceRoomSettingsCommand;

public class RemoveVoiceRoomSettingsCommandHandler(
    ILogger<RemoveVoiceRoomSettingsCommandHandler> logger,
    IRoomSettingsRepository roomVoiceSettingsRepository) : IRequestHandler<RemoveVoiceRoomSettingsCommand, BaseResult>
{
    public async Task<BaseResult> Handle(RemoveVoiceRoomSettingsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await roomVoiceSettingsRepository.RemoveRoomSettingsByGuildMemberDiscordIdAsync(request.GuildMemberDiscordId, cancellationToken);

            return BaseResult.Success(
                "Настройки личной комнаты были успешно удалены");
        }
        catch(ArgumentNullException ex)
        {
            logger.LogError(
                ex,
                "Настройки для участника с DiscordId {GuildMemberDiscordId} не были найдены",
                request.GuildMemberDiscordId);

            return BaseResult.Fail(
                "Не удалось удалить настройки личной комнаты, так как они не были заданы", 
                new Error(ErrorCodes.NOT_FOUND, 
                ex.Message));
        }
        
    }
}
