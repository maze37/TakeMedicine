using CSharpFunctionalExtensions;

namespace TakeMedicine.Domain.ValueObjects;

public class Schedule : ValueObject
{
    private readonly List<TimeOnly> _times;
    private readonly List<DayOfWeek> _days;
    
    public IReadOnlyCollection<TimeOnly> Times => _times;
    public IReadOnlyCollection<DayOfWeek> Days => _days;

    private Schedule(
        IEnumerable<TimeOnly> times,
        IEnumerable<DayOfWeek> days
    )
    {
        _times = times.Distinct().OrderBy(t=> t).ToList();
        _days = days.Distinct().ToList();
    }

    public static Result<Schedule> Create(
        IEnumerable<TimeOnly> times,
        IEnumerable<DayOfWeek> days)
    {
        ArgumentNullException.ThrowIfNull(times);
        ArgumentNullException.ThrowIfNull(days);

        var timeList = times.ToList();
        var dayList = days.ToList();

        if (!timeList.Any())
            return Result.Failure<Schedule>("Нужно указать хотя бы одно время.");

        if (!dayList.Any())
            return Result.Failure<Schedule>("Нужно указать хотя бы один день недели.");
        
        return Result.Success(new Schedule(timeList, dayList));
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        foreach (var time in _times)
            yield return time;

        foreach (var day in _days)
            yield return day;
    }
}