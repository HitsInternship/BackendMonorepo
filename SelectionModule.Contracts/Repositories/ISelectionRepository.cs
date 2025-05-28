using SelectionModule.Domain.Entites;
using Shared.Contracts.Repositories;

namespace SelectionModule.Contracts.Repositories;

public interface ISelectionRepository : IBaseEntityRepository<SelectionEntity>
{
    new Task<SelectionEntity> GetByIdAsync(Guid id);
}