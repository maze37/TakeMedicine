using TakeMedicine.Application;
using TakeMedicine.Infrastructure;

namespace TakeMedicine.Web;

internal static class Inject
{
    internal static IServiceCollection ConfigureApp(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddApplication()
            .AddInfrastructure(configuration)
            .AddSwaggerGen()
            .AddEndpointsApiExplorer()
            .AddControllers();
        
        return services;
    }
}