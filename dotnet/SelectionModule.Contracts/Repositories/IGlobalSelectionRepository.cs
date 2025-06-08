using SelectionModule.Domain.Entites;
using Shared.Contracts.Repositories;

namespace SelectionModule.Contracts.Repositories;

public interface IGlobalSelectionRepository : IBaseEntityRepository<GlobalSelection>
{
    Task<GlobalSelection?> GetActiveSelectionAsync();
}