using CSharpFunctionalExtensions;

namespace TakeMedicine.Domain.ValueObjects;

public class Username : ValueObject
{
    public string Value { get; private set; }

    private Username(string value)
    {
        Value = value;
    }

    public static Result<Username> Create(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            return Result.Failure<Username>("Имя пользователя не может быть пустым.");
        
        return Result.Success(new Username(username));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    public static implicit operator string(Username username) => username.Value;
}