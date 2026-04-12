using MediatR;

using A_MNY9M._1_Core.Results;

namespace A_MNY9M._2_Application.Commands.SetupGuildVoiceRoom;

public class SetupGuildVoiceRoomCommand : IRequest<BaseResult>
{
    public ulong GuildMemberDiscordId { get; set; }
    public string? RoomName { get; set; }
    public int? MembersLimit { get; set; }
    public string? Region { get; set; }
}
