using MediatR;

namespace NotificationModule.Contracts.Commands;

public record SendChangePasswordMessageCommand(string Code) : IRequest<Unit>;