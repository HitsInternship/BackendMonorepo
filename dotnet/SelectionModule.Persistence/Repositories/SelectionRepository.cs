using Microsoft.EntityFrameworkCore;
using SelectionModule.Contracts.Repositories;
using SelectionModule.Domain.Entites;
using SelectionModule.Infrastructure;
using Shared.Persistence.Repositories;

namespace SelectionModule.Persistence.Repositories;

public class SelectionRepository(SelectionDbContext context)
    : BaseEntityRepository<SelectionEntity>(context), ISelectionRepository
{
    new public async Task<SelectionEntity> GetByIdAsync(Guid id)
    {
        return await DbSet.Include(x => x.Candidate)
                   .Include(x => x.Comments)
                   .FirstOrDefaultAsync(x => x.Id == id) ??
               throw new InvalidOperationException();
    }

    public async Task<SelectionEntity?> GetByCandidateIdAsync(Guid candidateId)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.CandidateId == candidateId);
    }

    public async Task<bool> CheckIfStudentHasSelectionAsync(Guid studentId)
    {
        return await DbSet.AnyAsync(x => x.Candidate.StudentId == studentId);
    }
    
    public new async Task<IQueryable<SelectionEntity>> ListAllAsync()
    {
        return await Task.FromResult(DbSet.Include(x => x.Candidate)
            .Include(x => x.GlobalSelection)
            .AsNoTracking());
    }
}