using Discord;

namespace A_MNY9M.Integration.Discord.Abstractions;

public interface IDiscordV2ComponentsBuilder
{
    Task<MessageComponent> BuildWelcomeMessageComponentAsync();
    Task<MessageComponent> BuildRulesMessageComponentAsync();
}
