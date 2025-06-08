using DeanModule.Contracts.Dtos.Requests;
using MediatR;

namespace DeanModule.Contracts.Commands.Application;

public record UpdateApplicationCommand(
    Guid ApplicationId,
    ApplicationRequestDto ApplicationRequestDto,
    Guid UserId,
    List<string> Roles)
    : IRequest<Unit>;