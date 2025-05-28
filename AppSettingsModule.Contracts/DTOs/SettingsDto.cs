using AppSettingsModule.Domain.Entities;
using AppSettingsModule.Domain.Enums;


namespace AppSettingsModule.Contracts.DTOs
{
    public class SettingsDto
    {
        public Theme theme { get; set; }
        public Language language { get; set; }

        public SettingsDto() { }

        public SettingsDto(Settings settings)
        {
            theme = settings.theme;
            language = settings.language;
        }
    }
}
