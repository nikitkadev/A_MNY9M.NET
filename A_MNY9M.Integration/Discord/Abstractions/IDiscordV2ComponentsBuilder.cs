using Discord;

namespace A_MNY9M.Integration.Discord.Abstractions;

public interface IDiscordV2ComponentsBuilder
{
    Task<MessageComponent> BuildHubMessageComponentAsync();
    Task<MessageComponent> BuildRulesMessageComponentAsync();
    Task<MessageComponent> BuildRolesMessageComponentAsync();
    Task<MessageComponent> BuildColorSwitcherMessageComponentAsync();
    MessageComponent BuildDefaultMessageComponent(string text);
}
