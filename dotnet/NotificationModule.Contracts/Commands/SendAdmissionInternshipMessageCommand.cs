using MediatR;

namespace NotificationModule.Contracts.Commands;

public record SendAdmissionInternshipMessageCommand(string CompanyName, string Position) : IRequest<Unit>;