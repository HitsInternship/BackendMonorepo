using AppSettingsModule.Contracts.Commands;
using AppSettingsModule.Contracts.DTOs;
using AppSettingsModule.Contracts.Repositories;
using AppSettingsModule.Domain.Entities;
using MediatR;
using AppSettingsModule.Domain.Enums;

namespace AppSettingsModule.Application.Handlers
{
    public class CreateSettingsCommandHandler : IRequestHandler<CreateSettingsCommand, SettingsDto>
    {
        private readonly ISettingsRepository _settingsRepository;

        public CreateSettingsCommandHandler(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        public async Task<SettingsDto> Handle(CreateSettingsCommand request, CancellationToken cancellationToken)
        {
            var settings = new Settings()
            {
                userId = request.userId,
                theme = Theme.Light,
                language = Language.Ru
            };

            await _settingsRepository.AddAsync(settings);

            return new SettingsDto(settings);
        }
    }
}
