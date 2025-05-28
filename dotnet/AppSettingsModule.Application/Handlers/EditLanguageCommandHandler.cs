using AppSettingsModule.Contracts.Commands;
using AppSettingsModule.Contracts.DTOs;
using AppSettingsModule.Contracts.Repositories;
using MediatR;
using Shared.Domain.Exceptions;

namespace AppSettingsModule.Application.Handlers
{
    public class EditLanguageCommandHandler : IRequestHandler<EditLanguageCommand, SettingsDto>
    {
        private readonly ISettingsRepository _settingsRepository;

        public EditLanguageCommandHandler(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        public async Task<SettingsDto> Handle(EditLanguageCommand request, CancellationToken cancellationToken)
        {
            var settings = await _settingsRepository.GetSettingsByUserIdAsync(request.userId)
                ?? throw new NotFound("Settings not found");

            settings.language = request.language;
            await _settingsRepository.UpdateAsync(settings);

            return new SettingsDto(settings);
        }
    }
}
