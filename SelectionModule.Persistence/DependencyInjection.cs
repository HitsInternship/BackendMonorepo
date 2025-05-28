using Microsoft.Extensions.DependencyInjection;
using SelectionModule.Contracts.Repositories;
using SelectionModule.Persistence.Repositories;

namespace SelectionModule.Persistence;

public static class DependencyInjection
{
    public static void AddSelectionModulePersistence(this IServiceCollection services)
    {
        services.AddTransient<IVacancyResponseRepository, VacancyResponseRepository>();
        services.AddTransient<ICandidateRepository, CandidateRepository>();
        services.AddTransient<ISelectionRepository, SelectionRepository>();
        services.AddTransient<IPositionRepository, PositionRepository>();
        services.AddTransient<IVacancyRepository, VacancyRepository>();
        services.AddTransient<ISelectionCommentRepository, SelectionCommentRepository>();
        services.AddTransient<IVacancyResponseCommentRepository, VacancyResponseCommentRepository>();
    }
}