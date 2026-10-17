using Discord;

namespace A_MNY9M.Integration.Discord.Commands.Builders;

public static class SetSourceVoiceChannelCommandBuilder
{
    public static SlashCommandProperties Build()
    {
        return new SlashCommandBuilder()
            .WithName("set-source-voice-channel")
            .WithDescription("Настраивает канал-источник для авто-лобби")
            .AddOption(
                option: new SlashCommandOptionBuilder()
                    .WithRequired(true)
                    .WithType(ApplicationCommandOptionType.Channel))
            .Build();

    }
}
