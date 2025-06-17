using SelectionModule.Domain.Entites;
using Shared.Contracts.Repositories;

namespace SelectionModule.Contracts.Repositories;

public interface IVacancyRepository : IBaseEntityRepository<VacancyEntity>
{
    Task<List<VacancyEntity>> GetByCompanyAsync(Guid id);
}