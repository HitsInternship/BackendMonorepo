using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserModule.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddUserModuleInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserModuleDbContext>(options =>
                options.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING") ??
                                  configuration.GetConnectionString("HitsInternship")));
        }

        public static void AddUserModuleInfrastructure(this IServiceProvider services)
        {
            using var serviceScope = services.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<UserModuleDbContext>();
            dbContext?.Database.Migrate();
        }
    }
}