
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeMedicine.Domain.Entities;

namespace TakeMedicine.Infrastructure.Configurations;

public class MedicineConfigurations : IEntityTypeConfiguration<Medicine>
{
    public void Configure(EntityTypeBuilder<Medicine> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.NameOfPill)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.StartedWhen)
            .IsRequired();

        builder.Property(x => x.FinishWhen)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();
        
        builder.ComplexProperty(x => x.Schedule, schedule =>
        {
            schedule.Property<List<TimeOnly>>("_times")
                .HasColumnName("Times")
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(TimeOnly.Parse)
                        .ToList());

            schedule.Property<List<DayOfWeek>>("_days")
                .HasColumnName("Days")
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(d => Enum.Parse<DayOfWeek>(d))
                        .ToList());
        });
    }
}