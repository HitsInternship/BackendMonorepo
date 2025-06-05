using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SelectionModule.Infrastructure;

public static class DependencyInjection
{
    public static void AddSelectionModuleInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SelectionDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("HitsInternship")));
    }

    public static void AddSelectionModuleInfrastructure(this IServiceProvider services)
    {
        using var serviceScope = services.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<SelectionDbContext>();
        dbContext?.Database.Migrate();
    }
}