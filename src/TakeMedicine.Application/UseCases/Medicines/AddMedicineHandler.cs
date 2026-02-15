using CSharpFunctionalExtensions;
using TakeMedicine.Application.Abstractions;
using TakeMedicine.Domain.Entities;
using TakeMedicine.Domain.ValueObjects;

namespace TakeMedicine.Application.UseCases.Medicines;

public class AddMedicineHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public AddMedicineHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        Guid userId,
        string nameOfPill,
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        IEnumerable<TimeOnly> times,
        IEnumerable<DayOfWeek> days,
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
            return Result.Failure("Пользователь не найден");

        // Вернет успешный результат с объектом NameOfPill (а он содержит внутри себя string)
        var nameResult = NameOfPill.Create(nameOfPill);
        if (nameResult.IsFailure)
            return Result.Failure(nameResult.Error);

        var scheduleResult = Schedule.Create(times, days);
        if (scheduleResult.IsFailure)
            return Result.Failure(scheduleResult.Error);
        
        var medicineResult = Medicine.Create(
            Guid.NewGuid(),
            nameResult.Value,
            startDate,
            endDate,
            scheduleResult.Value
        );

        if (medicineResult.IsFailure)
            return Result.Failure(medicineResult.Error);

        var addResult = user.AddMedicine(medicineResult.Value);
        if (addResult.IsFailure)
            return addResult;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
