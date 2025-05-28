using SelectionModule.Contracts.Repositories;
using SelectionModule.Domain.Entites;
using SelectionModule.Infrastructure;
using Shared.Persistence.Repositories;

namespace SelectionModule.Persistence.Repositories;

public class PositionRepository(SelectionDbContext context)
    : BaseEntityRepository<PositionEntity>(context), IPositionRepository
{
}