using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PracticeModule.Application;
using PracticeModule.Controllers.PracticeControllers;
using PracticeModule.Infrastructure;
using PracticeModule.Persistence;

namespace PracticeModule.Controllers
{
    public static class DependencyInjection
    {
        public static void AddPracticeModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPracticeModuleInfrastructure(configuration);
            services.AddPracticeModulePersistence();
            services.AddPracticeModuleApplication();
            services.AddScoped<ExelService>();


            // services.AddSwaggerGen(options =>
            // {
            //     var assemblyName = Assembly.GetAssembly(typeof(PracticeController))?.GetName().Name;
            //     var xmlFilename = $"{assemblyName}.xml";
            //     options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            // });

        }

        public static void UsePracticeModule(this IServiceProvider services)
        {
            services.AddPracticeModuleInfrastructure();
        }
    }
}
