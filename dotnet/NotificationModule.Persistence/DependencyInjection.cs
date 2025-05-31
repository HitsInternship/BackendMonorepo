using Microsoft.Extensions.DependencyInjection;
using NotificationModule.Contracts.Repositories;
using NotificationModule.Persistence.Repositories;

namespace NotificationModule.Persistence;

public static class DependencyInjection
{
    public static void AddNotificationModulePersistence(this IServiceCollection services)
    {
        services.AddTransient<IMessageRepository, MessageRepository>();
    }
}