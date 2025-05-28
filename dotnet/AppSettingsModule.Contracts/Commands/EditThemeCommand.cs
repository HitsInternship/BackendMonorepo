using AppSettingsModule.Contracts.DTOs;
using AppSettingsModule.Domain.Enums;
using MediatR;


namespace AppSettingsModule.Contracts.Commands
{
    public record EditThemeCommand (Guid userId, Theme Theme) : IRequest<SettingsDto>;
}
