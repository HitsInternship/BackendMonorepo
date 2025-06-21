using MediatR;
using NotificationModule.Domain.Enums;

namespace NotificationModule.Contracts.Commands;

public record SendDeadlineMessageCommand(List<string> Emails, DateOnly DeadLineDate, DeadLineType EventType) : IRequest<Unit>;