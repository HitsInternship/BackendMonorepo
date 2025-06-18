using AppSettingsModule.Contracts.DTOs;
using AppSettingsModule.Contracts.Queries;
using AppSettingsModule.Contracts.Repositories;
using AppSettingsModule.Domain.Entities;
using AppSettingsModule.Domain.Enums;
using MediatR;
using Shared.Domain.Exceptions;


namespace AppSettingsModule.Application.Handlers
{
    public class GetSettingsQueryHandler : IRequestHandler<GetSettingsQuery, SettingsDto>
    {
        private readonly ISettingsRepository _settingsRepository;

        public GetSettingsQueryHandler(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }
        public async Task<SettingsDto> Handle(GetSettingsQuery request, CancellationToken cancellationToken)
        {
            var settings = await _settingsRepository.GetSettingsByUserIdAsync(request.userId);

            if (settings == null)
            {
                settings = new Settings
                {
                    theme = Theme.Light,
                    language = Language.Ru
                };
            }

            return new SettingsDto(settings);
        }
    }
}
