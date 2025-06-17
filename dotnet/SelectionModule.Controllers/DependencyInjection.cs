using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SelectionModule.Application;
using SelectionModule.Contracts.Dtos.Responses;
using SelectionModule.Controllers.Controllers;
using SelectionModule.Infrastructure;
using SelectionModule.Persistence;

namespace SelectionModule.Controllers;

public static class DependencyInjection
{
    public static void AddSelectionModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSelectionModuleInfrastructure(configuration);
        services.AddSelectionModulePersistence();
        services.AddSelectionModuleApplication(configuration);

        services.AddSwaggerGen(options =>
        {
            var selectionModuleControllersXmlFilename = $"{typeof(VacancyController).Assembly.GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, selectionModuleControllersXmlFilename));
            
            var selectionModuleDtosXmlFiles = $"{typeof(CandidateDto).Assembly.GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, selectionModuleDtosXmlFiles));
        });
    }

    public static void UseSelectionModule(this IServiceProvider services)
    {
        services.AddSelectionModuleInfrastructure();
    }
}