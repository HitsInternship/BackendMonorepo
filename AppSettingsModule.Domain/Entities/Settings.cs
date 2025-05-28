using AppSettingsModule.Domain.Enums;
using Shared.Domain.Entites;


namespace AppSettingsModule.Domain.Entities
{
    public class Settings : BaseEntity
    {
        public Guid userId { get; set; }
        public Theme theme { get; set; } = Theme.Light;
        public Language language { get; set; } = Language.Ru;
    }
}
