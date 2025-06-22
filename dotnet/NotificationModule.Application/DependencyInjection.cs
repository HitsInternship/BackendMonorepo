using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace NotificationModule.Application;

public static class DependencyInjection 
{
    public static void AddNotificationModuleApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
    }
}