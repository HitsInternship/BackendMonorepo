using DeanModule.Contracts.Repositories;
using DeanModule.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DeanModule.Persistence;

public static class DependencyInjection
{
    public static void AddDeanModulePersistence(this IServiceCollection services)
    {
        services.AddTransient<IDeanMemberRepository, DeanMemberRepository>();
        services.AddTransient<IApplicationRepository, ApplicationRepository>();
        services.AddTransient<ISemesterRepository, SemesterRepository>();
        services.AddTransient<IStreamSemesterRepository, StreamSemesterRepository>();
    }
}