using MediatR;

using A_MNY9M._1_Core.Results;

namespace A_MNY9M._2_Application.Commands.RemoveVoiceRoomSettingsCommand;

public class RemoveVoiceRoomSettingsCommand : IRequest<BaseResult>
{
    public ulong GuildMemberDiscordId { get; set; }
}
