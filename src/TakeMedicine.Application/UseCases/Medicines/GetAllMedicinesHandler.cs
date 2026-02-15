using CSharpFunctionalExtensions;
using TakeMedicine.Application.Abstractions;
using TakeMedicine.Application.DTOs;

namespace TakeMedicine.Application.UseCases.Medicines;

public class GetAllMedicinesHandler
{
    private readonly IUserRepository _userRepository;

    public GetAllMedicinesHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<List<MedicineDto>>> Handle(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
            return Result.Failure<List<MedicineDto>>("Пользователь не найден.");

        var medicines = user.Medicines
            .Where(m => m.IsActive)
            .Select(m => new MedicineDto
            {
                Id = m.Id,
                Name = m.NameOfPill.Value,
                StartDate = m.StartedWhen,
                EndDate = m.FinishWhen,
                Schedule = new ScheduleDto
                {
                    Times = m.Schedule.Times.ToList(),
                    Days = m.Schedule.Days.ToList()
                }
            }).ToList();

        return Result.Success(medicines);
    }
}