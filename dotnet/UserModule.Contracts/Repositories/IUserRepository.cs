using Shared.Contracts.Repositories;
using UserModule.Domain.Entities;

namespace UserModule.Contracts.Repositories
{
    public interface IUserRepository : IBaseEntityRepository<User>
    {
        public Task<User?> GetByEmailAsync(string email);
    }
}
