using MediatR;

namespace NotificationModule.Contracts.Commands;

public record SendChangePasswordMessageCommand(string Email, string Code) : IRequest<Unit>;