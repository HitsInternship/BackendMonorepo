using AppSettingsModule.Domain.Entities;
using Shared.Contracts.Repositories;


namespace AppSettingsModule.Contracts.Repositories
{
    public interface ISettingsRepository : IBaseEntityRepository<Settings>
    {
        Task<Settings> GetSettingsByUserIdAsync(Guid id);
    }
}
