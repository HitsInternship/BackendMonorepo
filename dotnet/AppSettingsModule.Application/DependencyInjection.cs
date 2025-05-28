using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AppSettingsModule.Application
{
    public static class DependencyInjection
    {
        public static void AddAppSettingsModuleApplication(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        }
    }
}
