using SelectionModule.Domain.Entites;
using Shared.Contracts.Repositories;

namespace SelectionModule.Contracts.Repositories;

public interface IPositionRepository : IBaseEntityRepository<PositionEntity>
{
}