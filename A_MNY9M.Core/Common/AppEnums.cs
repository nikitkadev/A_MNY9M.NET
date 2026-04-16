namespace A_MNY9M.Core.Common;

public enum ErrorCode
{
    None = 0,
    InternalError = 1,
    NotFound = 2,
    VariableIsNull = 3,
    RoleAssignmentFailed = 4,
    RoleRemovalFailed = 5,
    DatabaseError = 6,
}

public enum RoleType
{
    Server,
    Category,
    Unique,
    Color
}

[Flags]
public enum SentMessageType
{
    None = 0,
    Text = 1,
    Command = 2,
    Gif = 4,
    Image = 8,
    Sticker = 16,
}