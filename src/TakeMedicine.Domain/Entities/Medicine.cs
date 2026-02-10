using CSharpFunctionalExtensions;

namespace TakeMedicine.Domain.Entities;

public class Medicine : Entity<Guid>
{
    public string NameOfPill { get; set; } = string.Empty;
    public DateTimeOffset StartedWhen { get; set; }
    public DateTimeOffset FinishWhen { get; set; }
    public List<Reminder> Reminders { get; set; } = new();
    public bool IsActive { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}