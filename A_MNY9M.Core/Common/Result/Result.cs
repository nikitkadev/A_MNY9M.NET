using A_MNY9M.Core.Common.Enums;

namespace A_MNY9M.Core.Common.Result;

public record Result(
    bool IsSuccess,
    string ClientMessage,
    CustomError? Error)
{
    public bool IsFailure => !IsSuccess;

    public static Result Success(string clientMessage) => new(true, clientMessage, null);
    public static Result Fail(string clientMessage, CustomError error) => new(false, clientMessage, error);
}

public record Result<T>(
    T? Value,
    bool IsSuccess,
    string ClientMessage,
    CustomError? Error) : Result(IsSuccess, ClientMessage, Error)
{
    public static Result<T> Success(T? value, string clientMessage) => new(value, true, clientMessage, null);
    public static new Result<T> Fail(string clientMessage, CustomError error) => new (default, false, clientMessage, error);
}

public sealed record CustomError(
    CustomErrorType Type, 
    string Code,
    string Message);