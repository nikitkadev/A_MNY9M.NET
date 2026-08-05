using Discord.WebSocket;

namespace A_MNY9M.Integration.Discord.Abstractions;

public interface IDiscordResponseRenderer<TResult> 
{
    Task RenderAsync(SocketSlashCommand slashCommand, TResult result);
}