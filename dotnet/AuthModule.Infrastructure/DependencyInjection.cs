using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserInfrastructure;

public static class DependencyInjection
{
    public static void AddAuthModuleInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuthDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("HitsInternship")));
    }

    public static void AddAuthModuleInfrastructure(this IServiceProvider services)
    {
        using var serviceScope = services.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<AuthDbContext>();
        dbContext?.Database.Migrate();
    }
}
