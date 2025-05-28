using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AppSettingsModule.Infrastructure;
using AppSettingsModule.Application;
using AppSettingsModule.Persistence;

namespace AppSettingsModule.Controllers
{
    public static class DependencyInjection
    {
        public static void AddAppSettingsModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAppSettingsModuleInfrastructure(configuration);
            services.AddAppSettingsModulePersistence();
            services.AddAppSettingsModuleApplication();
        }

        public static void UseAppSettingsModule(this IServiceProvider services)
        {
            services.AddAppSettingsModuleInfrastructure();
        }
    }
}
