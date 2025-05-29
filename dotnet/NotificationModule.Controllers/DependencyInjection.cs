using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationModule.Infrastructure;

namespace NotificationModule.Controllers;

public static class DependencyInjection
{
    public static void AddNotificationModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddNotificationModuleInfrastructure(configuration);
    }
    
    public static void UseNotificationModule(this IServiceProvider services)
    {
        services.UseNotificationModuleInfrastructure();
    }
}