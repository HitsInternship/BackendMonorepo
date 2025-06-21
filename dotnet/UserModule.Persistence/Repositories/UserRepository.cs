using Microsoft.EntityFrameworkCore;
using Shared.Persistence.Repositories;
using UserModule.Domain.Entities;
using UserModule.Infrastructure;
using UserModule.Contracts;
using UserModule.Contracts.Repositories;

namespace UserModule.Persistence.Repositories
{
    public class UserRepository : BaseEntityRepository<User>, IUserRepository
    {
        private readonly UserModuleDbContext context;

        public UserRepository(UserModuleDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await context.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<List<User>> GetUserByName(string name, string surname)
        {
            var users = await context.Users
                .Where(u => u.Name == name && u.Surname == surname)
                .ToListAsync();

            return users;
        }

        public async Task<List<User>> GetByIdsAsync(List<Guid> ids)
        {
            return await DbSet.Where(u => ids.Contains(u.Id)).ToListAsync();
        }
    }
}