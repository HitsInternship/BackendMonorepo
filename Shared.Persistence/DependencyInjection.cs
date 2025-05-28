using Microsoft.Extensions.DependencyInjection;
using Shared.Contracts.Repositories;
using Shared.Persistence.Repositories;

namespace Shared.Persistence;

public static class DependencyInjection
{
    public static void AddGenericRepositories(this IServiceCollection services)
    {
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient(typeof(IBaseEntityRepository<>), typeof(BaseEntityRepository<>));
    }
}