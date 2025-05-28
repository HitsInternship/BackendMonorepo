using Microsoft.EntityFrameworkCore;
using Shared.Persistence.Repositories;
using UserModule.Contracts.Repositories;
using UserModule.Domain.Entities;
using UserModule.Domain.Enums;
using UserModule.Infrastructure;

namespace UserModule.Persistence.Repositories
{
    public class RoleRepository : BaseEntityRepository<Role>, IRoleRepository
    {
        private readonly UserModuleDbContext _context;
        public RoleRepository(UserModuleDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<Role> GetRoleAsync(RoleName roleName)
        {
            return _context.Roles.First(role => role.RoleName == roleName);
        }

        public async Task<List<Role>> GetRolesAsync(List<RoleName> roleNames)
        {
            return _context.Roles.Where(Role => roleNames.Contains(Role.RoleName)).ToList();
        }

        public async Task<List<Role>?> GetRolesByUserIdAsync(Guid userId)
        {
            return _context.Users.Where(user => user.Id == userId).Include(user => user.Roles).FirstOrDefault()?.Roles;
        }
    }
}
