using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace SelectionModule.Application;

public static class DependencyInjection
{
    public static void AddSelectionModuleApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        services.AddAutoMapper(typeof(MappingProfile));
    }
}