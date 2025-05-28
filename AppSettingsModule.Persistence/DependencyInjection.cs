using AppSettingsModule.Contracts.Repositories;
using Microsoft.Extensions.DependencyInjection;


namespace AppSettingsModule.Persistence
{
    public static class DependencyInjection
    {
        public static void AddAppSettingsModulePersistence(this IServiceCollection services)
        {
            services.AddTransient<ISettingsRepository, SettingsRepository>();
        }
    }
}
