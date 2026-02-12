using CSharpFunctionalExtensions;
using TakeMedicine.Domain.ValueObjects;

namespace TakeMedicine.Domain.Entities;

public class Medicine : Entity<Guid>
{
    public string NameOfPill { get; private set; } = string.Empty;
    public DateTimeOffset StartedWhen { get; private set; }
    public DateTimeOffset FinishWhen { get; private set; }
    public Schedule Schedule { get; private set; } = null!;
    public bool IsActive { get; private set; }

    private Medicine() : base(Guid.Empty) {}

    private Medicine(
        Guid id,
        string name,
        DateTimeOffset startedWhen,
        DateTimeOffset finishWhen,
        Schedule schedule) : base(id)
    {
        NameOfPill = name;
        StartedWhen = startedWhen;
        FinishWhen = finishWhen;
        Schedule = schedule;
        IsActive = true;
    }

    public static Result<Medicine> Create(
        Guid id,
        string name,
        DateTimeOffset startedWhen,
        DateTimeOffset finishWhen,
        Schedule schedule)
    {
        if (id == Guid.Empty)
            return Result.Failure<Medicine>("ID не может быть пустым Guid");
        
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Medicine>("Название таблетки не может быть пустым.");

        if (startedWhen > finishWhen)
            return Result.Failure<Medicine>("Время начала курса таблеток не может быть раньше конца курса.");
        
        return Result.Success(
            new Medicine(id, name, startedWhen, finishWhen, schedule));
    }
    
    public void Deactivate()
    {
        IsActive = false;
    }
}