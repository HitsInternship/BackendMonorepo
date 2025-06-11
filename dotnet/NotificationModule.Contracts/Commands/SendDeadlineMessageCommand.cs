using MediatR;
using NotificationModule.Domain.Enums;

namespace NotificationModule.Contracts.Commands;

public record SendDeadlineMessageCommand(string Email, DateOnly DeadLineDate, DeadLineType EventType) : IRequest<Unit>;
//todo: add job to send deadline message