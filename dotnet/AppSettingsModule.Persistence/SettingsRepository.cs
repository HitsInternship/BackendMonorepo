using AppSettingsModule.Contracts.Repositories;
using AppSettingsModule.Domain.Entities;
using AppSettingsModule.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shared.Persistence.Repositories;

namespace AppSettingsModule.Persistence
{
    public class SettingsRepository : BaseEntityRepository<Settings>, ISettingsRepository
    {
        private readonly AppSettingsDbContext context;
        public SettingsRepository(AppSettingsDbContext context) : base(context)
        {
            this.context = context;
        }

        public Task<Settings> GetSettingsByUserIdAsync(Guid id)
        {
            return context.Settings.FirstOrDefaultAsync(s => s.userId == id);
        }
    }
}
