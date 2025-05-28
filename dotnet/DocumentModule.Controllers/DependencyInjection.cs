using DocumentModule.Application;
using DocumentModule.Controllers.Controllers;
using DocumentModule.Infrastructure;
using DocumentModule.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentModule.Controllers
{
    public static class DependencyInjection
    {
        public static void AddDocumentModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDocumentModuleInfrastructure(configuration);
            services.AddDocumentModulePersistence();
            services.AddDocumentModuleApplication();

            services.AddSwaggerGen(options =>
            {
                var companyModuleControllersXmlFilename = $"{typeof(DocumentController).Assembly.GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, companyModuleControllersXmlFilename));
            });

        }

        public static void UseDocumentModule(this IServiceProvider services)
        {
            services.AddDocumentModuleInfrastructure();
        }
    }
}
