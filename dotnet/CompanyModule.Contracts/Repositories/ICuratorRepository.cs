using CompanyModule.Domain.Entities;
using Shared.Contracts.Repositories;

namespace CompanyModule.Contracts.Repositories
{
    public interface ICuratorRepository : IBaseEntityRepository<Curator>
    {
        public Task<List<Curator>> GetCuratorsByCompany(Company company);

        public Task<bool> CheckIfUserIsCurator(Guid companyId, Guid userId);
    }
}