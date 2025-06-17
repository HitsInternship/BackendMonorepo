using SelectionModule.Domain.Entites;
using Shared.Contracts.Repositories;

namespace SelectionModule.Contracts.Repositories;

public interface IVacancyResponseRepository : IBaseEntityRepository<VacancyResponseEntity>
{
    Task SoftDeleteByCandidateAsync(Guid candidateId);
    new Task<VacancyResponseEntity> GetByIdAsync(Guid id);
    Task<List<VacancyResponseEntity>> GetByCandidateIdAsync(Guid candidateId);
    Task<bool> CheckIfExistsByUserIdAsync(Guid userId, Guid requestVacancyId);
}