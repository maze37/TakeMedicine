namespace TakeMedicine.Application.DTOs;

public class MedicineDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public ScheduleDto Schedule { get; set; } = new();
}