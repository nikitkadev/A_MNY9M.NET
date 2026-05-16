using Discord;

namespace A_MNY9M.Integration.Discord.Abstractions;

public interface IDiscordV2ComponentsBuilder
{
    Task<MessageComponent> BuildHubMessageComponentAsync();
    Task<MessageComponent> BuildRulesMessageComponentAsync();
    Task<MessageComponent> BuildRolesMessageComponentAsync();
    Task<MessageComponent> BuildColorSwitcherMessageComponentAsync();
    Task<MessageComponent> BuildWelcomeMessageComponent(string userMention);
    MessageComponent BuildDefaultMessageComponent(string text);
}
