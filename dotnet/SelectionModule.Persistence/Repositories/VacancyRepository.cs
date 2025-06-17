using Microsoft.EntityFrameworkCore;
using SelectionModule.Contracts.Repositories;
using SelectionModule.Domain.Entites;
using SelectionModule.Infrastructure;
using Shared.Persistence.Repositories;

namespace SelectionModule.Persistence.Repositories;

public class VacancyRepository(SelectionDbContext context)
    : BaseEntityRepository<VacancyEntity>(context), IVacancyRepository
{
    public async Task<List<VacancyEntity>> GetByCompanyAsync(Guid id)
    {
        return await DbSet.Where(v => v.CompanyId == id).ToListAsync();
    }
}