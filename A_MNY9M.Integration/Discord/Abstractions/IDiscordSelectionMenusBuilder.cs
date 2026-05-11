using Discord;

namespace A_MNY9M.Integration.Discord.Abstractions;

public interface IDiscordSelectionMenusBuilder
{
    Task<SelectMenuBuilder> RolesSelectionMenu();
}
