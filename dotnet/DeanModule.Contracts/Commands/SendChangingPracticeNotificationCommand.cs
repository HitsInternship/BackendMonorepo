using DeanModule.Domain.Entities;
using MediatR;

namespace DeanModule.Contracts.Commands;

public record SendChangingPracticeNotificationCommand(ApplicationEntity Application) : IRequest<Unit>;