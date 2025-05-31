using MediatR;
using NotificationModule.Domain.Enums;

namespace NotificationModule.Contracts.Commands;

public record SendDeadlineMessageCommand(DateTime DeadLineDate, DeadLineType EventType) : IRequest<Unit>;