using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationModule.Infrastructure;
using NotificationModule.Kafka;
using NotificationModule.Persistence;

namespace NotificationModule.Controllers;

public static class DependencyInjection
{
    public static void AddNotificationModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddNotificationModuleInfrastructure(configuration);
        services.AddNotificationModulePersistence();
        services.AddKafka(configuration);
    }
    
    public static async Task UseNotificationModule(this IServiceProvider services)
    {
        services.UseNotificationModuleInfrastructure();
        await services.UseKafka();
    }
}