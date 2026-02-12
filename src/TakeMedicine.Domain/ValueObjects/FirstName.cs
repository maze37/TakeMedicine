using System.Collections.Generic;
using CSharpFunctionalExtensions;
using TakeMedicine.Domain.Entities;

namespace TakeMedicine.Domain.ValueObjects;

public class FirstName : ValueObject
{
    public string Value { get; private set; }

    private FirstName(string value)
    {
        Value = value;
    }

    public static Result<FirstName> Create(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Result.Failure<FirstName>("Имя не может быть пустым.");
        
        return Result.Success(new FirstName(firstName));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    public static implicit operator string(FirstName firstName) => firstName.Value;
}