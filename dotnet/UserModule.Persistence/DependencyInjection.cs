using Microsoft.Extensions.DependencyInjection;
using UserModule.Contracts.Repositories;
using UserModule.Persistence.Repositories;

namespace UserModule.Persistence
{
    public static class DependencyInjection
    {
        public static void AddUserModulePersistence(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
        }
    }
}
