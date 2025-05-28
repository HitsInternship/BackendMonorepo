using AppSettingsModule.Contracts.Commands;
using AppSettingsModule.Contracts.DTOs;
using AppSettingsModule.Contracts.Repositories;
using AppSettingsModule.Infrastructure;
using MediatR;
using Shared.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSettingsModule.Application.Handlers
{
    public class EditThemeCommandHandler : IRequestHandler<EditThemeCommand, SettingsDto>
    {
        private readonly ISettingsRepository _settingsRepository;

        public EditThemeCommandHandler(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        public async Task<SettingsDto> Handle(EditThemeCommand request, CancellationToken cancellationToken)
        {
            var settings = await _settingsRepository.GetSettingsByUserIdAsync(request.userId)
                ?? throw new NotFound("Settings not found");

            settings.theme = request.Theme;
            await _settingsRepository.UpdateAsync(settings);

            return new SettingsDto(settings);
        }
    }
}
