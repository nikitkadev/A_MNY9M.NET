using MediatR;

using Discord.WebSocket;

namespace A_MNY9M._2_Application.Events.SlashCommandExecuted
{
    public class SlashCommandExecuted(SocketSlashCommand socketSlashCommand) : INotification
    {
        public SocketSlashCommand SocketSlashCommand { get; set; } = socketSlashCommand;
    }
}
