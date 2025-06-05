using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StudentModule.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddStudentModuleInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<StudentModuleDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("HitsInternship")));
        }

        public static void AddStudentModuleInfrastructure(this IServiceProvider services)
        {
            using var serviceScope = services.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<StudentModuleDbContext>();
            dbContext?.Database.Migrate();
        }
    }
}