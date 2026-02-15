using CSharpFunctionalExtensions;
using TakeMedicine.Application.Abstractions;
using TakeMedicine.Domain.Entities;
using TakeMedicine.Domain.ValueObjects;

namespace TakeMedicine.Application.UseCases.Users;

public class RegisterUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public RegisterUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        Guid id,
        long telegramId,
        string firstName,
        string username,
        CancellationToken cancellationToken = default)
    {
        var telegramIdVo = TelegramId.Create(telegramId).Value;
        var existingUser = await _userRepository.GetByIdTelegramIdAsync(
            telegramIdVo, cancellationToken);

        if (existingUser != null)
            return Result.Success();

        var user = User.Create( 
            id,
            telegramIdVo,
            FirstName.Create(firstName).Value,
            Username.Create(username).Value,
            TimeZoneInfo.Utc
        ).Value;

        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}