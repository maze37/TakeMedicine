using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeMedicine.Domain.Entities;
using TakeMedicine.Domain.ValueObjects;

namespace TakeMedicine.Infrastructure.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Id
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
        
        builder.Property(x => x.CreatedWhen)
            .IsRequired();

        // TelegramId
        builder.Property(x => x.TelegramId)
            .HasConversion(
                v => v.Value,
                v => TelegramId.Create(v).Value)
            .IsRequired();

        // FirstName
        builder.Property(x => x.FirstName)
            .HasConversion(
                v => v.Value,
                v => FirstName.Create(v).Value)
            .IsRequired();

        // Username
        builder.Property(x => x.Username)
            .HasConversion(
                v => v.Value,
                v => Username.Create(v).Value)
            .IsRequired(false);

        // TimeZone
        builder.Property(x => x.TimeZone)
            .HasConversion(
                v => v.Id,
                v => TimeZoneInfo.FindSystemTimeZoneById(v))
            .IsRequired();
        
        builder
            .HasMany<Medicine>("_medicines")
            .WithOne()
            .HasForeignKey("UserId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}