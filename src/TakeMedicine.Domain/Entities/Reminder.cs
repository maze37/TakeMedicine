using CSharpFunctionalExtensions;

namespace TakeMedicine.Domain.Entities;

public class Reminder : Entity<Guid>
{
    public DateTimeOffset TimeOfDay { get; set; }
    public List<DayOfWeek> DaysOfWeek { get; set; } = new();
    public bool IsEnabled { get; set; }
    
    public Guid MedicineId { get; set; }
    public Medicine Medicine { get; set; } = null!;
}