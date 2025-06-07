using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NotificationModule.Infrastructure;

public static class DependencyInjection 
{
    public static void AddNotificationModuleInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<NotificationModuleDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("HitsInternship")));
    }

    public static void UseNotificationModuleInfrastructure(this IServiceProvider services)
    {
        using var serviceScope = services.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<NotificationModuleDbContext>();
        dbContext?.Database.Migrate();
    }
}