using AppSettingsModule.Contracts.DTOs;
using MediatR;


namespace AppSettingsModule.Contracts.Commands
{
    public record CreateSettingsCommand(Guid userId) : IRequest<SettingsDto>;
}
