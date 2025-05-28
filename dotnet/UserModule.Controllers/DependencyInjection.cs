using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserModule.Application;
using UserModule.Controllers.Controllers;
using UserModule.Infrastructure;
using UserModule.Persistence;

namespace UserModule.Controllers
{
    public static class DependencyInjection
    {
        public static void AddUserModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddUserModuleInfrastructure(configuration);
            services.AddUserModulePersistence();
            services.AddUserModuleApplication();

            services.AddSwaggerGen(options =>
            {
                var userModuleControllersXmlFilename = $"{typeof(UserController).Assembly.GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, userModuleControllersXmlFilename));
            });
        }

        public static async Task UseUserModule(this IServiceProvider services)
        {
            await services.AddUserModuleInfrastructure();
        }
    }
}