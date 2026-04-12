namespace A_MNY9M.Core.Enums;

[Flags]
public enum SentMessageType
{
    None = 0,
    Text = 1,
    Command = 2,
    Gifs = 4,
    Image = 8,
    Sticker = 16,
}
