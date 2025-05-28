using Microsoft.EntityFrameworkCore;
using SelectionModule.Contracts.Repositories;
using SelectionModule.Domain.Entites;
using SelectionModule.Infrastructure;
using Shared.Persistence.Repositories;

namespace SelectionModule.Persistence.Repositories;

public class VacancyResponseRepository(SelectionDbContext context)
    : BaseEntityRepository<VacancyResponseEntity>(context), IVacancyResponseRepository
{
    public async Task SoftDeleteByCandidateAsync(Guid candidateId)
    {
        var entities = await DbSet
            .Where(e => e.CandidateId == candidateId && !e.IsDeleted)
            .ToListAsync();

        foreach (var entity in entities)
        {
            entity.IsDeleted = true;
        }

        await Context.SaveChangesAsync();
    }

    public new async Task<VacancyResponseEntity> GetByIdAsync(Guid id)
    {
        return await DbSet
                   .Include(x => x.Candidate)
                   .Include(x => x.Vacancy)
                   .FirstOrDefaultAsync(x => x.Id == id)
               ?? throw new InvalidOperationException();
    }
}