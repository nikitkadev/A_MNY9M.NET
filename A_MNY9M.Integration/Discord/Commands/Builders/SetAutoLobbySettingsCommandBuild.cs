using A_MNY9M.Integration.Constants;
using Discord;

namespace A_MNY9M.Integration.Discord.Commands.Builders;

public static class SetAutoLobbySettingsCommandBuild
{
    public static SlashCommandProperties Build()
    {
        return new SlashCommandBuilder()
            .WithName(DiscordCommandNames.SetAutoVoiceChannelSettings)
            .WithDescription(DiscordCommandDescriptions.SetAutoVoiceChannelSettings)
            .AddOption(
                option: new SlashCommandOptionBuilder()
                    .WithName(DiscordCommandOptionNames.AutoVoiceChannelDefaultName)
                    .WithDescription(DiscordCommandOptionDescriptions.AutoVoiceChannelDefaultName)
                    .WithRequired(true)
                    .WithType(ApplicationCommandOptionType.String)
                    .WithMaxLength(255))
            .AddOption(
                option: new SlashCommandOptionBuilder()
                    .WithName(DiscordCommandOptionNames.UserLimit)
                    .WithDescription(DiscordCommandOptionDescriptions.UserLimit)
                    .WithRequired(false)
                    .WithType(ApplicationCommandOptionType.Integer)
                    .WithMaxValue(99))
            .AddOption(
                option: new SlashCommandOptionBuilder()
                    .WithName(DiscordCommandOptionNames.Region)
                    .WithDescription(DiscordCommandOptionDescriptions.Region)
                    .WithRequired(false)
                    .WithType(ApplicationCommandOptionType.String)
                    .AddChoice("test", "test"))
            .Build();
    }
}
