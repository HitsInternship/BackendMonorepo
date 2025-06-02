using MediatR;
using NotificationModule.Domain.Enums;

namespace NotificationModule.Contracts.Commands;

public record SendDeadlineMessageCommand(string Email, DateTime DeadLineDate, DeadLineType EventType) : IRequest<Unit>;