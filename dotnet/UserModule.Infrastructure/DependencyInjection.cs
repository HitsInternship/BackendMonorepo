using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserModule.Infrastructure.DbInitializer;

namespace UserModule.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddUserModuleInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserModuleDbContext>(options =>
                options.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING") ??
                                  configuration.GetConnectionString("HitsInternship")));

            services.AddScoped<IUserInitializer, UserInitializer>();
        }

        public static async Task AddUserModuleInfrastructure(this IServiceProvider services)
        {
            using var serviceScope = services.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<UserModuleDbContext>();
            await dbContext?.Database.MigrateAsync()!;

            using var scope = services.CreateScope();
            var initializer = scope.ServiceProvider.GetRequiredService<IUserInitializer>();
            await initializer.InitializeAsync();
        }
    }
}