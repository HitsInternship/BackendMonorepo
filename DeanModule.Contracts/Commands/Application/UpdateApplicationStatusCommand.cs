using DeanModule.Domain.Enums;
using MediatR;

namespace DeanModule.Contracts.Commands.Application;

public record UpdateApplicationStatusCommand(Guid ApplicationId, ApplicationStatus Status) : IRequest<Unit>;