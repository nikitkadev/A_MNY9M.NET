namespace A_MNY9M.Integration.Discord.Commands.Content;

public sealed class DiscordCommandNames
{
    public const string SetSourceVoiceChannel = "set-source-voicechannel";
    public const string SetAutoVoiceChannelSettings = "set-autolobby-settings";
}

public sealed class DiscordCommandOptionNames
{
    public const string VoiceChannel = "voice-channel";
    public const string AutoVoiceChannelDefaultName = "default-name";
    public const string UserLimit = "user-limit";
    public const string Region = "region";
}

public sealed class DiscordCommandDescriptions
{
    public const string SetSourceVoiceChannel = "Позволяет указать канал-источник для создания авто-лобби.";
    public const string SetAutoVoiceChannelSettings = "Позволяет назначить параметры личной комнаты по умолчанию.";
}

public sealed class DiscordCommandOptionDescriptions
{
    public const string VoiceChannel = "Голосовой канал, который станет каналом-источником.";
    public const string AutoVoiceChannelDefaultName = "Имя создаваемой комнаты.";
    public const string UserLimit = "Лимит подключаемых пользователей.";
    public const string Region = "Сервер, через который идет подключение в комнате.";

}
