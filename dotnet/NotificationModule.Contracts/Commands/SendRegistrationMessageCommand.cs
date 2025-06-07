using MediatR;

namespace NotificationModule.Contracts.Commands;

public record SendRegistrationMessageCommand(string Email, string Password) : IRequest<Unit>;