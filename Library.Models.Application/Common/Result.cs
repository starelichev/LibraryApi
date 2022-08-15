namespace Library.Models.Application.Common;

public class Result
{
    public bool Success { get; set; }
    public string? Message { get; set; }

    protected Result(bool success, string error)
    {
        Success = success;
        Message = error;
    }
    public static Result Ok()
    {
        return new Result(true, string.Empty);
    }

    public static Result Fail(string message)
    {
        return new Result(false, message);
    }
    public static Result<T> Fail<T>(string message)
    {
        return new Result<T>(false, message);
    }

    public static Result<T> Ok<T>(T value)
    {
        return new Result<T>(value, true, string.Empty);
    }

}

public class Result<T> : Result
{
    protected internal Result(T value, bool success, string message)
        : base(success, message)
    {
        Success = success;
        Message = message;
        Value = value;
    }

    protected internal Result(bool success, string error) : base(success, error)
    {
        Success = success;
        Message = error;
    }

    public T Value { get; set; }
}