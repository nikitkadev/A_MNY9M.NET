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
                    .WithName("voice-channel")
                    .WithDescription("Голосовой канал, который станет каналом-источником.")
                    .WithRequired(true)
                    .WithType(ApplicationCommandOptionType.Channel)
                    .AddChannelType(ChannelType.Voice))
            .Build();

    }
}
