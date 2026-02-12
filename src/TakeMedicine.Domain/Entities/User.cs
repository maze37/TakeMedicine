using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using TakeMedicine.Domain.ValueObjects;

namespace TakeMedicine.Domain.Entities;

public class User : Entity<Guid>
{
    private readonly List<Medicine> _medicines = new();

    public TelegramId TelegramId { get; private set; } = null!;
    public FirstName FirstName { get; private set; } = null!;
    public Username Username { get; private set; } = null!;
    public TimeZoneInfo TimeZone { get; private set; } = null!;
    public DateTimeOffset CreatedWhen { get; private set; }
    
    public IReadOnlyCollection<Medicine> Medicines => _medicines;
    
    private User() : base(Guid.Empty) { }

    private User(Guid id) : base(id) { }

    public static Result<User> Create(
        Guid id,
        TelegramId telegramId,
        FirstName firstName,
        Username username,
        TimeZoneInfo timeZone)
    {
        if (id == Guid.Empty)
            return Result.Failure<User>("ID не может быть пустым Guid.");

        ArgumentNullException.ThrowIfNull(telegramId);
        ArgumentNullException.ThrowIfNull(firstName);
        ArgumentNullException.ThrowIfNull(username);
        ArgumentNullException.ThrowIfNull(timeZone);

        var user = new User(id)
        {
            TelegramId = telegramId,
            FirstName = firstName,
            Username = username,
            TimeZone = timeZone,
            CreatedWhen = DateTimeOffset.UtcNow
        };

        return Result.Success(user);
    }

    public Result AddMedicine(Medicine medicine)
    {
        if (_medicines.Any(m => m.NameOfPill == medicine.NameOfPill && m.IsActive))
            return Result.Failure("Лекарство с таким именем уже существует.");
        
        _medicines.Add(medicine);
        return Result.Success();
    }

    public Result RemoveMedicine(Guid medicineId)
    {
        var medicine = _medicines.FirstOrDefault(m => m.Id == medicineId);

        if (medicine is null)
            return Result.Failure("Таблетка не найдена.");
        
        medicine.Deactivate();
        return Result.Success();
    }
        
}