using Discord;

namespace A_MNY9M.Integration.Discord.Abstractions;

public interface IDiscordMessagesSender
{
    Task SendWelcomeMessageToNewMemberAsync(MessageComponent messageComponent);
}
