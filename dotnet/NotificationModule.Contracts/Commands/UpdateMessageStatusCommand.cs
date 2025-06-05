using MediatR;
using NotificationModule.Domain.Enums;

namespace NotificationModule.Contracts.Commands;

public record UpdateMessageStatusCommand(Guid MessageId, MessageStatus Status) : IRequest<Unit>;