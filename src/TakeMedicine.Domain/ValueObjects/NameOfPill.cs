using CSharpFunctionalExtensions;

namespace TakeMedicine.Domain.ValueObjects;

public class NameOfPill : ValueObject
{
    public string Value { get; private set; }

    public NameOfPill(string value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public static Result<NameOfPill> Create(string nameOfPill)
    {
        if (string.IsNullOrWhiteSpace(nameOfPill))
            return Result.Failure<NameOfPill>("Имя не может быть пустым.");

        return Result.Success(new NameOfPill(nameOfPill));
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}