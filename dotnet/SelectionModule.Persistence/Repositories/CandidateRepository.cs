using Microsoft.EntityFrameworkCore;
using SelectionModule.Contracts.Repositories;
using SelectionModule.Domain.Entites;
using SelectionModule.Infrastructure;
using Shared.Persistence.Repositories;

namespace SelectionModule.Persistence.Repositories;

public class CandidateRepository(SelectionDbContext context)
    : BaseEntityRepository<CandidateEntity>(context), ICandidateRepository
{
    public async Task<CandidateEntity?> GetCandidateByStudentIdAsync(Guid userId)
    {
        return await DbSet
            .Include(x => x.Selection)
            .FirstOrDefaultAsync(x => x.StudentId == userId);
    }

    public async Task<CandidateEntity?> GetCandidateByUserIdAsync(Guid userId)
    {
        return await DbSet
            .Include(x => x.Selection)
            .FirstOrDefaultAsync(x => x.UserId == userId);
    }

    public async Task<CandidateEntity?> GetCandidateByIdAsync(Guid candidateId)
    {
        return await DbSet
            .Include(x => x.Selection)
            .FirstOrDefaultAsync(x => x.Id == candidateId);
    }
}