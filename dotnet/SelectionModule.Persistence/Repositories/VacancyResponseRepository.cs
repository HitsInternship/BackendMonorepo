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
                   .Include(x => x.Comments)
                   .FirstOrDefaultAsync(x => x.Id == id)
               ?? throw new InvalidOperationException();
    }

    public async Task<List<VacancyResponseEntity>> GetByCandidateIdAsync(Guid candidateId)
    {
        return await DbSet.Include(x => x.Vacancy)
            .ThenInclude(x => x.Position)
            .Include(x => x.Candidate)
            .Where(x => x.CandidateId == candidateId)
            .ToListAsync();
    }

    public async Task<bool> CheckIfExistsByUserIdAsync(Guid userId, Guid requestVacancyId)
    {
        return await DbSet.AnyAsync(x => x.VacancyId == requestVacancyId && x.Candidate.UserId == userId);
    }
}