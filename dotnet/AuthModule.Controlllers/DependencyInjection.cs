using AuthModel.Service;
using AuthModel.Service.Interface;
using AuthModel.Service.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserInfrastructure;

namespace AuthModule.Controlllers;

public static class DependencyInjection
{
    public static void AddAuthModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthModuleInfrastructure(configuration);
        services.AddAuthModuleApplication();
        services.AddScoped<IHashService, HashService>();
    }

    public static void UseAuthModule(this IServiceProvider services)
    {
        services.AddAuthModuleInfrastructure();
    }
}