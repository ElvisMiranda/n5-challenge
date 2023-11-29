using System.Diagnostics.CodeAnalysis;

namespace N5.Challenge.Domain.Abstractions;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public ErrorOr.Error Error { get; }

    protected internal Result(bool isSuccess, ErrorOr.Error error)
    {
        switch (isSuccess)
        {
            case true when error != Abstractions.Error.None:
                throw new InvalidOperationException();
            case false when error == Abstractions.Error.None:
                throw new InvalidOperationException();
            default:
                IsSuccess = isSuccess;
                Error = error;
                break;
        }
    }

    public static Result Success() => new(true, Abstractions.Error.None);

    public static Result Failure(ErrorOr.Error error) => new(false, error);

    public static Result<TValue> Success<TValue>(TValue value) => new(value, true,  Abstractions.Error.None);

    public static Result<TValue> Failure<TValue>(ErrorOr.Error error) => new(default, false, error);

    public static Result<TValue> Create<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(Abstractions.Error.NullValue);
}

public class Result<TValue> : Result
{

    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, ErrorOr.Error error) : base(isSuccess, error)
    {
        _value = value;
    }

    [NotNull]
    public TValue Value => IsSuccess
        ? _value
        : throw new InvalidOperationException("The value of a failure result cannot be accessed");

    public static implicit operator Result<TValue>(TValue? value) => Create(value);
}