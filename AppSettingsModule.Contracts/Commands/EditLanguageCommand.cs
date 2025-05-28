using AppSettingsModule.Contracts.DTOs;
using AppSettingsModule.Domain.Enums;
using MediatR;

namespace AppSettingsModule.Contracts.Commands
{
    public record EditLanguageCommand(Guid userId, Language language) : IRequest<SettingsDto>;
}
