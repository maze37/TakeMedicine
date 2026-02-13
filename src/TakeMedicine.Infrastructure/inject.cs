using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TakeMedicine.Application.Abstractions;

namespace TakeMedicine.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        
        var connectionString = configuration.GetConnectionString("TakeMedicinePostgreSQL")
                               ?? throw new InvalidOperationException(
                                   "Connection string 'TakeMedicinePostgreSQL' not found in configuration.");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsql =>
            {
                npgsql.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                npgsql.CommandTimeout(30);
            });
        });

        services.AddScoped<IUserRepository, IUserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}