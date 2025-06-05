using AuthModule.Controlllers;
using CompanyModule.Controllers;
using DeanModule.Controllers;
using DocumentModule.Controllers;
using PracticeModule.Controllers;
using SelectionModule.Controllers;
using Shared.Extensions;
using StudentModule.Controllers;
using UserModule.Controllers;
using AppSettingsModule.Controllers;
using HitsInternship.Api.Extensions.GlobalInitializer;
using NotificationModule.Controllers;

namespace HitsInternship.Api.Extensions;

public static class Modules
{
    public static void AddApplicationModules(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment builderEnvironment)
    {
        services.AddSharedModule(configuration);
        services.AddDeanModule(configuration);
        services.AddUserModule(configuration);
        services.AddDocumentModule(configuration);
        services.AddAuthModule(configuration);
        services.AddStudentModule(configuration);
        services.AddSelectionModule(configuration);
        services.AddCompanyModule(configuration);
        services.AddPracticeModule(configuration);
        services.AddAppSettingsModule(configuration);
        services.AddNotificationModule(configuration, builderEnvironment);
    }

    public static async Task UseApplicationModules(this IServiceProvider services)
    {
        services.UseDeanModule();
        services.UseUserModule();
        services.UseDocumentModule();
        services.UseCompanyModule();
        services.UseAuthModule();
        services.UseStudentModule();
        services.UseSelectionModule();
        services.UsePracticeModule();
        services.UseAppSettingsModule();
        await services.UseNotificationModule();
        services.InitializeDatabases().Wait();
    }
}