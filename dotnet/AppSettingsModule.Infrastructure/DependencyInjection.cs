using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace AppSettingsModule.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddAppSettingsModuleInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppSettingsDbContext>(options =>
                options.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING") ?? configuration.GetConnectionString("HitsInternship")));
        }

        public static void AddAppSettingsModuleInfrastructure(this IServiceProvider services)
        {
            using var serviceScope = services.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<AppSettingsDbContext>();
            dbContext?.Database.Migrate();
        }
    }
}
