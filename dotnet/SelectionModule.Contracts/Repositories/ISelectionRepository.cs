using SelectionModule.Domain.Entites;
using Shared.Contracts.Repositories;

namespace SelectionModule.Contracts.Repositories;

public interface ISelectionRepository : IBaseEntityRepository<SelectionEntity>
{
    new Task<SelectionEntity> GetByIdAsync(Guid id);
    Task<SelectionEntity?> GetByCandidateIdAsync(Guid candidateId);
    Task<bool> CheckIfStudentHasSelectionAsync(Guid studentId);
    new Task<IQueryable<SelectionEntity>> ListAllAsync();
}