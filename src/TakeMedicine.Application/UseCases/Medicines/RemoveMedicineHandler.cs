using CSharpFunctionalExtensions;
using TakeMedicine.Application.Abstractions;
using TakeMedicine.Domain.Entities;

namespace TakeMedicine.Application.UseCases.Medicines;

public class RemoveMedicineHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveMedicineHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        Guid userId,
        Guid medicineId,
        CancellationToken cancellationToken = default
    )
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
            return Result.Failure("Пользователь не найден.");
        
        var removeResult = user.RemoveMedicine(medicineId);
        if (removeResult.IsFailure)
            return removeResult;
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return removeResult;
    }
}