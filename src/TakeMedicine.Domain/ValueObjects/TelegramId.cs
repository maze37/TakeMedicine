using CSharpFunctionalExtensions;

namespace TakeMedicine.Domain.ValueObjects;

public class TelegramId : ValueObject
{
    public long Value { get; private set; }
    
    private TelegramId(long value)
    {
        Value = value;
    }

    public static Result<TelegramId> Create(long telegramId)
    {
        if (telegramId <= 0)
            return Result.Failure<TelegramId>("Телеграм ID не может быть пустым.");
        
        return Result.Success(new TelegramId(telegramId));
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    public static implicit operator long(TelegramId telegramId) => telegramId.Value;
    public override string ToString() => Value.ToString();
}