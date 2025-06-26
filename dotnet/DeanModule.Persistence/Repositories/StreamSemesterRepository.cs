using DeanModule.Contracts.Repositories;
using DeanModule.Domain.Entities;
using DeanModule.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shared.Persistence.Repositories;

namespace DeanModule.Persistence.Repositories;

public class StreamSemesterRepository(DeanModuleDbContext context)
    : BaseEntityRepository<StreamSemesterEntity>(context), IStreamSemesterRepository
{
    private IStreamSemesterRepository _streamSemesterRepositoryImplementation;

    public async Task<IEnumerable<StreamSemesterEntity>> GetByStreamIdAsync(Guid streamId)
    {
        return await DbSet.Include(x => x.SemesterEntity).Where(s => s.StreamId == streamId).ToListAsync();
    }

    public async Task<StreamSemesterEntity?> GetBySemesterIdAsync(Guid semesterId, int semesterNumber)
    {
        return await DbSet.FirstOrDefaultAsync(x =>
            x.StreamId == semesterId && x.Number == semesterNumber);
    }

    public new Task<IQueryable<StreamSemesterEntity>> ListAllAsync()
    {
        return Task.FromResult(DbSet.Include(x => x.SemesterEntity).AsQueryable());
    }

    public async Task SoftDeleteRangBySemesterAsync(Guid semesterId)
    {
        var semesterEntities = DbSet
            .Where(x => x.SemesterId == semesterId && !x.IsDeleted);

        foreach (var entity in semesterEntities)
        {
            entity.IsDeleted = true;
        }

        await Context.SaveChangesAsync();
    }

    public async Task SoftDeleteRangeByStreamAsync(Guid streamId)
    {
        var semesterEntities = DbSet
            .Where(x => x.StreamId == streamId && !x.IsDeleted);

        foreach (var entity in semesterEntities)
        {
            entity.IsDeleted = true;
        }

        await Context.SaveChangesAsync();
    }

    public async Task<bool> CheckIfStreamSemesterExistsAsync(Guid semesterId, Guid streamId)
    {
        return await DbSet.AnyAsync(x => x.StreamId == streamId && x.StreamId == semesterId);
    }
}