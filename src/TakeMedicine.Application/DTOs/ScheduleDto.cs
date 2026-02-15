namespace TakeMedicine.Application.DTOs;

public class ScheduleDto
{
    public List<TimeOnly> Times { get; set; } = new();
    public List<DayOfWeek> Days { get; set; } = new();
}