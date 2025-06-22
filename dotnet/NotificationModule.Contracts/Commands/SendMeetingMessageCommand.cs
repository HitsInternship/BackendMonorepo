using MediatR;

namespace NotificationModule.Contracts.Commands;

public record SendMeetingMessageCommand(Guid AppointmentId, DateTime DateTime, string CompanyName, List<string> Emails)
    : IRequest<Unit>;