using DeanModule.Contracts.Repositories;
using DeanModule.Domain.Entities;
using DeanModule.Domain.Enums;
using DeanModule.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shared.Persistence.Repositories;

namespace DeanModule.Persistence.Repositories;

public class ApplicationRepository(DeanModuleDbContext context)
    : BaseEntityRepository<ApplicationEntity>(context), IApplicationRepository
{
    public async Task<IEnumerable<ApplicationEntity>> GetByStudentIdAsync(Guid studentId)
    {
        return await DbSet.Where(a => a.StudentId == studentId).ToListAsync();
    }

    public async Task<IEnumerable<ApplicationEntity>> GetByCompanyId(Guid companyId)
    {
        return await DbSet.Where(a => a.CompanyId == companyId).ToListAsync();
    }

    public async Task<IEnumerable<ApplicationEntity>> GetByPositionIdAsync(Guid positionId)
    {
        return await DbSet.Where(a => a.PositionId == positionId).ToListAsync();
    }

    public async Task<IEnumerable<ApplicationEntity>> GetByStatusAsync(ApplicationStatus status)
    {
        return await DbSet.Where(a => a.Status == status).ToListAsync();
    }
}