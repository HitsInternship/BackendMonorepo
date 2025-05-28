using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentModule.Infrastructure;
using StudentModule.Persistence;
using StudentModule.Application;

namespace StudentModule.Controllers
{
    public static class DependencyInjection
    {
        public static void AddStudentModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStudentModuleInfrastructure(configuration);
            services.AddStudentModulePersistence();
            services.AddStudentModuleApplication();
        }

        public static void UseStudentModule(this IServiceProvider services)
        {
            services.AddStudentModuleInfrastructure();
        }
    };
}
