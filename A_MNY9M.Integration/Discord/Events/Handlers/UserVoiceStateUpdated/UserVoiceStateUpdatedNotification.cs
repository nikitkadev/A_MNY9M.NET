using Discord.WebSocket;
using MediatR;

namespace A_MNY9M.Integration.Discord.Events.Handlers.UserVoiceStateUpdated;

public class UserVoiceStateUpdatedNotification(
    SocketUser user,
    SocketVoiceState oldState, 
    SocketVoiceState newState) : INotification
{
    public SocketUser User { get; set; } = user;
    public SocketVoiceState OldState { get; set; } = oldState;
    public SocketVoiceState NewState { get; set; } = newState;
}