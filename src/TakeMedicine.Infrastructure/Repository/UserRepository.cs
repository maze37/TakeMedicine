using Microsoft.EntityFrameworkCore;
using TakeMedicine.Application.Abstractions;
using TakeMedicine.Domain.Entities;
using TakeMedicine.Domain.ValueObjects;

namespace TakeMedicine.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .Include(u => u.Medicines)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<User?> GetByIdTelegramIdAsync(
        TelegramId telegramId, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .Include(u => u.Medicines)
            .FirstOrDefaultAsync(u => u.TelegramId == telegramId, cancellationToken)
            .ConfigureAwait(false);
    }

    public Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(user);
        _context.Users.AddAsync(user, cancellationToken);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(user);
        _context.Users.Remove(user);
        return Task.CompletedTask;
    }
}