using MediatR;
using NotificationModule.Domain.Enums;

namespace NotificationModule.Contracts.Commands;

public record SendChangingPracticeCommand(
    string Email,
    string OldCompanyName,
    string OldPosition,
    string NewCompanyName,
    string NewPosition,
    ChangingPracticeStatusType StatusType) : IRequest<Unit>;