using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PracticeModule.Infrastructure;

public static class DependencyInjection
{
    public static void AddPracticeModuleInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PracticeDbContext>(options =>
            options.UseNpgsql( configuration.GetConnectionString("HitsInternship")));
    }

    public static void AddPracticeModuleInfrastructure(this IServiceProvider services)
    {
        using var serviceScope = services.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<PracticeDbContext>();
        dbContext?.Database.Migrate();
    }
}
