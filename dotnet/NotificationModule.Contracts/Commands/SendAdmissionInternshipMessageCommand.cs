using MediatR;

namespace NotificationModule.Contracts.Commands;

public record SendAdmissionInternshipMessageCommand(string Email, string CompanyName, string Position) : IRequest<Unit>;