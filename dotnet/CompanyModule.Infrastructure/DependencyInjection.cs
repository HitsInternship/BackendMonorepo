using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyModule.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddCompanyModuleInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CompanyModuleDbContext>(options =>
                options.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING") ?? configuration.GetConnectionString("HitsInternship")));
        }

        public static void AddCompanyModuleInfrastructure(this IServiceProvider services)
        {
            using var serviceScope = services.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<CompanyModuleDbContext>();
            dbContext?.Database.Migrate();
        }
    }
}
