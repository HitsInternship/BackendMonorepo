using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace CompanyModule.Application
{
    public static class DependencyInjection
    {
        public static void AddCompanyModuleApplication(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddAutoMapper(typeof(CompanyModuleMappingProfile));
        }
    }
}
