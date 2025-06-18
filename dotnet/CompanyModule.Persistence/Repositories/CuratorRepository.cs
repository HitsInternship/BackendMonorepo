using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using CompanyModule.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shared.Persistence.Repositories;

namespace CompanyModule.Persistence.Repositories
{
    public class CuratorRepository(CompanyModuleDbContext context)
        : BaseEntityRepository<Curator>(context), ICuratorRepository
    {
        public async Task<List<Curator>> GetCuratorsByCompany(Company company)
        {
            return await DbSet.Where(curator => curator.Company == company).ToListAsync();
        }

        public async Task<bool> CheckIfUserIsCurator(Guid companyId, Guid userId)
        {
            return await DbSet.AnyAsync(x => x.UserId == userId && x.Company.Id == companyId);
        }

        public async Task<Curator?> GetCuratorByUserId(Guid id)
        {
            return await DbSet.Include(x => x.Company).FirstOrDefaultAsync(curator => curator.UserId == id);
        }
    }
}