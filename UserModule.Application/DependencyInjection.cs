using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace UserModule.Application
{
    public static class DependencyInjection
    {
        public static void AddUserModuleApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddAutoMapper(typeof(UserModuleMappingProfile));
        }
    }
}