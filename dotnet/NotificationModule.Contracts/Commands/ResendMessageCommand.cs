using MediatR;
using NotificationModule.Domain.Entites;

namespace NotificationModule.Contracts.Commands;

public record ResendMessageCommand(Message Message) : IRequest<Unit>;