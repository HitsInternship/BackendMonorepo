using DeanModule.Application;
using DeanModule.Contracts.Dtos.Responses;
using DeanModule.Controllers.Controllers;
using DeanModule.Infrastructure;
using DeanModule.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeanModule.Controllers;

public static class DependencyInjection
{
    public static void AddDeanModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDeanModuleInfrastructure(configuration);
        services.AddDeanModulePersistence();
        services.AddApplication();

        services.AddSwaggerGen(options =>
        {
            var deanModuleControllersXmlFilename = $"{typeof(SemesterController).Assembly.GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, deanModuleControllersXmlFilename));

            var deanModuleDtosXmlFiles = $"{typeof(SemesterResponseDto).Assembly.GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, deanModuleDtosXmlFiles));
        });
    }

    public static void UseDeanModule(this IServiceProvider services)
    {
        services.AddDeanModuleInfrastructure();
    }
}