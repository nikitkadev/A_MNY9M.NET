using Discord;
using A_MNY9M.Integration.Constants;

namespace A_MNY9M.Integration.Discord.Commands.Builders;

public static class SetSourceVoiceChannelCommandBuilder
{
    public static SlashCommandProperties Build()
    {
        return new SlashCommandBuilder()
            .WithName(DiscordCommandNames.SetSourceVoiceChannel)
            .WithDescription(DiscordCommandDescriptions.SetSourceVoiceChannel)
            .AddOption(
                option: new SlashCommandOptionBuilder()
                    .WithName(DiscordCommandOptionNames.VoiceChannel)
                    .WithDescription(DiscordCommandOptionDescriptions.VoiceChannel)
                    .WithRequired(true)
                    .WithType(ApplicationCommandOptionType.Channel)
                    .AddChannelType(ChannelType.Voice))
            .Build();

    }
}
