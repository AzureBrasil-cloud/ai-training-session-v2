using System.Diagnostics.CodeAnalysis;
using ContosoAcai.Application;

namespace PowerPilotChat.Application;

public sealed record Error(Guid Code, string? RawMessage = null, IDictionary<string, object>? Args = null)
{
    public string Message {
        get
        {
            if (string.IsNullOrWhiteSpace(RawMessage))
            {
                return string.Empty;
            }

            var args = Args ?? new Dictionary<string, object>();

            string result = RawMessage;

            foreach (var arg in args)
            {
                var oldValue = "{" + arg.Key + "}";
                var newValue = arg.Value.ToString();
                
                result = result.Replace(oldValue, newValue);
            }

            return result;
        }
    }
}

public class Result
{
    public static Result Success() => new(true, Errors.None);
    protected Result(bool isSuccess, Error error)
    {
        switch (isSuccess)
        {
            case true when error != Errors.None:
            case false when error == Errors.None:
                throw new InvalidOperationException();
            default:
                IsSuccess = isSuccess;
                Error = error;
                break;
        }
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }
}

public class Result<T> : Result
{
    private readonly T? _value;

    protected internal Result(T? value, bool isSuccess, Error error) : base(isSuccess, error)
        => _value = value;

    [NotNull]
    public T Value => _value! ?? throw new InvalidOperationException("Result has no value");
    
    public static Result<T> Success<T>(T value) => new(value, true, Errors.None);
    public static Result<T> Failure(Error error) => new(default, false, error);
    public static Result<T> Create(T? value) =>
        value is not null ? Success(value) : Failure(Errors.NullValue);

    public static implicit operator Result<T>(T? value) => Create(value);
}

public static class ResultExtensions
{
    public static T Match<T>(
        this Result result,
        Func<T> onSuccess,
        Func<Error, T> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result.Error);
    }
}