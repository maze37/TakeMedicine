using CSharpFunctionalExtensions;
using TakeMedicine.Application.Abstractions;
using TakeMedicine.Domain.Entities;
using TakeMedicine.Domain.ValueObjects;

namespace TakeMedicine.Application.UseCases.User;

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
        var telegramIdResult = TelegramId.Create(telegramId);
        if (telegramIdResult.IsFailure)
            return telegramIdResult;

        var existingUser = await _userRepository.GetByIdTelegramIdAsync(
            telegramIdResult.Value, cancellationToken);

        if (existingUser != null)
            return Result.Failure("User already exists");

        var firstNameResult = FirstName.Create(firstName);
        if (firstNameResult.IsFailure)
            return firstNameResult;

        var usernameResult = Username.Create(username);
        if (usernameResult.IsFailure)
            return usernameResult;
        
        var userResult = User.Create(
            id,
            telegramIdResult.Value,
            firstNameResult.Value,
            usernameResult.Value,
            TimeZoneInfo.Utc);

        if (userResult.IsFailure)
            return userResult;

        await _userRepository.AddAsync(userResult.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}