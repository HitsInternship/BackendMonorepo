using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PracticeModule.Application;
using PracticeModule.Infrastructure;

namespace PracticeModule.Controllers
{
    public static class DependencyInjection
    {
        public static void AddPracticeModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPracticeModuleInfrastructure(configuration);
            services.AddPracticeModuleApplication();

        }

        public static void UsePracticeModule(this IServiceProvider services)
        {
            services.AddPracticeModuleInfrastructure();
        }
    }
}
