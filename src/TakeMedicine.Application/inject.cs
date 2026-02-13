using Microsoft.Extensions.DependencyInjection;

namespace TakeMedicine.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        return services;
    }
}