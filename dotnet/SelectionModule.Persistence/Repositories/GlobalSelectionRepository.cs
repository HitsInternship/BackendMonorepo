using Microsoft.EntityFrameworkCore;
using SelectionModule.Contracts.Repositories;
using SelectionModule.Domain.Entites;
using SelectionModule.Infrastructure;
using Shared.Persistence.Repositories;

namespace SelectionModule.Persistence.Repositories;

public class GlobalSelectionRepository(SelectionDbContext context)
    : BaseEntityRepository<GlobalSelection>(context), IGlobalSelectionRepository
{
    public async Task<GlobalSelection?> GetActiveSelectionAsync()
    {
        return await DbSet.Include(x => x.Selections)
            .FirstOrDefaultAsync(x => x.IsDeleted == false);
    }
}