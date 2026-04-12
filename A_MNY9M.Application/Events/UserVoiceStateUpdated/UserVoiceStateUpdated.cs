using MediatR;
using Discord.WebSocket;

namespace A_MNY9M._2_Application.Events.UserVoiceStateUpdated;

public class UserVoiceStateUpdated(SocketUser socketUser, SocketVoiceState oldState, SocketVoiceState newState) : INotification
{
    public SocketUser SocketUser { get; } = socketUser;
    public SocketVoiceState OldState { get; } = oldState;
    public SocketVoiceState NewState { get; } = newState;
}
