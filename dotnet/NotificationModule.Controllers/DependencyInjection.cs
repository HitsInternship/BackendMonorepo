using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotificationModule.Application;
using NotificationModule.Infrastructure;
using NotificationModule.Kafka;
using NotificationModule.Persistence;
using Shared.Extensions;

namespace NotificationModule.Controllers;

public static class DependencyInjection
{
    public static void AddNotificationModule(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment builderEnvironment)
    {
        services.AddNotificationModuleInfrastructure(configuration);
        services.AddNotificationModulePersistence();
        services.AddKafka(configuration);
        services.AddNotificationModuleApplication();

        services.AddSwaggerGen(c =>
        {
            c.DocInclusionPredicate((docName, apiDesc) =>
            {
                var env = builderEnvironment;

                if (!env.IsEnvironment("Testing"))
                {
                    var controllerActionDescriptor =
                        apiDesc.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;
                    if (controllerActionDescriptor != null)
                    {
                        var hasAttr = controllerActionDescriptor.ControllerTypeInfo
                            .GetCustomAttributes(typeof(EnvironmentOnlyAttribute), true)
                            .Any();
                        if (hasAttr)
                            return false;
                    }
                }

                return true;
            });
        });
    }

    public static async Task UseNotificationModule(this IServiceProvider services)
    {
        services.UseNotificationModuleInfrastructure();
        await services.UseKafka();
    }
}