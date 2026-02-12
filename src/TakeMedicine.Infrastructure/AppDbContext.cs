using Microsoft.EntityFrameworkCore;
using TakeMedicine.Domain.Entities;

namespace TakeMedicine.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    DbSet<User> Users { get; set; }
    DbSet<Medicine> Medicines { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder, nameof(modelBuilder));
        
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}