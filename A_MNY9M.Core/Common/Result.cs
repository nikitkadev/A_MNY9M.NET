namespace A_MNY9M.Core.Common;

public class Result(
    bool isSuccess,
    string clientMessage,
    string errorMessage,
    Guid errorId)
{
    public bool IsSuccess { get; } = isSuccess;
    public string ClientMessage { get; } = clientMessage;
    public string ErrorMessage { get; } = errorMessage;
    public Guid ErrorId { get; } = errorId;

    public bool IsFailure => !IsSuccess;

    public static Result Success(string clientMessage) => new(true, clientMessage, string.Empty, Guid.Empty);
    public static Result Fail(string clientMessage, string errorMessage, Guid errorId) => new(false, clientMessage, errorMessage, errorId);
}

public class Result<T>(
    T? value,
    bool isSuccess,
    string clientMessage,
    string errorMessage,
    Guid errorId) : Result(isSuccess, clientMessage, errorMessage, errorId)
{
    public T? Value { get; } = value;

    public static Result<T> Success(T? value, string clientMessage) => new(value, true, clientMessage, string.Empty, Guid.Empty);
    public static new Result<T> Fail(string clientMessage, string errorMessage, Guid errorId) => new(default, false, clientMessage, errorMessage, errorId);
}