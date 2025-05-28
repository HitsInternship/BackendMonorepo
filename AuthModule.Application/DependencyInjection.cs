using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AuthModel.Service
{
    public static class DependencyInjection
    {
        public static void AddAuthModuleApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
