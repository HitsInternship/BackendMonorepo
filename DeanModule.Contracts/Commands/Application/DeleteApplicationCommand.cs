using MediatR;

namespace DeanModule.Contracts.Commands.Application;

public record DeleteApplicationCommand(Guid ApplicationId, bool IsArchive, Guid UserId, List<string> roles): IRequest<Unit>;