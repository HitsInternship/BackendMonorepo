using Shared.Contracts.Repositories;
using UserModule.Domain.Entities;

namespace UserModule.Contracts.Repositories
{
    public interface IUserRepository : IBaseEntityRepository<User>
    {
        public Task<User?> GetByEmailAsync(string email);

        public Task<List<User>> GetUserByName(string name, string surname);
        
        public Task<List<User>> GetByIdsAsync(List<Guid> ids);
    }
}
