using Shared.Contracts.Repositories;
using UserModule.Domain.Entities;
using UserModule.Domain.Enums;

namespace UserModule.Contracts.Repositories
{
    public interface IRoleRepository : IBaseEntityRepository<Role>
    {
        public Task<Role> GetRoleAsync(RoleName roleName);
        public Task<List<Role>> GetRolesAsync(List<RoleName> roleNames);

        public Task<List<Role>> GetRolesByUserIdAsync(Guid userId);
    }
}