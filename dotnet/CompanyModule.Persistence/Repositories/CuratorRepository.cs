using CompanyModule.Contracts.Repositories;
using CompanyModule.Domain.Entities;
using CompanyModule.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shared.Persistence.Repositories;

namespace CompanyModule.Persistence.Repositories
{
    public class CuratorRepository : BaseEntityRepository<Curator>, ICuratorRepository
    {
        private readonly CompanyModuleDbContext _context;

        public CuratorRepository(CompanyModuleDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Curator>> GetCuratorsByCompany(Company company)
        {
            return _context.Curators.Where(curator => curator.Company == company).ToList();
        }

        public async Task<bool> CheckIfUserIsCurator(Guid companyId, Guid userId)
        {
            return await DbSet.AnyAsync(x => x.UserId == userId && x.Company.Id == companyId);
        }
    }
}