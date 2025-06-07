using Shared.Domain.Entites;

namespace Shared.Contracts.Repositories;

public interface IBaseEntityRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> GetByIdAsync(Guid id);
    Task<bool> CheckIfExistsAsync(Guid id);
    Task<IQueryable<TEntity>> ListAllAsync();
    Task<IQueryable<TEntity>> ListAllActiveAsync();
    Task<IQueryable<TEntity>> ListAllArchivedAsync();
    Task SoftDeleteAsync(Guid id);
    Task<int> CountAsync();

}