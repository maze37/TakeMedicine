using TakeMedicine.Domain.Entities;
using TakeMedicine.Domain.ValueObjects;

namespace TakeMedicine.Application.Abstractions;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetByIdTelegramIdAsync(
        TelegramId telegramId, CancellationToken cancellationToken = default);
    Task AddAsync(User user, CancellationToken cancellationToken = default);
    Task DeleteAsync(User user, CancellationToken cancellationToken = default);
}