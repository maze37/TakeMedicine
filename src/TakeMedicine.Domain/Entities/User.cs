using CSharpFunctionalExtensions;
using TakeMedicine.Domain.ValueObjects;

namespace TakeMedicine.Domain.Entities;

public class User : Entity<Guid>
{
    public TelegramId TelegramId { get; private set; } = null!;
    public Username Username { get; private set; } = null!;
    public FirstName FirstName { get; private set; } = null!;
    public DateTimeOffset CreatedWhen { get; private set; }
    public TimeZoneInfo TimeZone { get; private set; } = TimeZoneInfo.Local;
    public List<Medicine> Medicines { get; private set; } = new();
    
    private User() : base(Guid.Empty) { }

    private User(Guid id) : base(id) { }
    
    public static Result<User> Create(
        Guid id,
        long telegramId,
        string firstName,
        string username)
    {
        // Валидация и создание Value Objects
        var telegramIdResult = TelegramId.Create(telegramId);
        if (telegramIdResult.IsFailure)
            return Result.Failure<User>("Телеграм ID не может быть пустым.");
        
        var firstNameResult = FirstName.Create(firstName);
        if (firstNameResult.IsFailure)
            return Result.Failure<User>("Имя не может быть пустым.");
        
        var usernameResult = Username.Create(username);
        if (usernameResult.IsFailure)
            return Result.Failure<User>("Имя пользователя не может быть пустым.");
        
        
    }
}