using MediatR;

namespace DeanModule.Contracts.Commands.ApplicationComments;

public record AddApplicationCommentCommand(Guid ApplicationId, string Comment, Guid UserId, List<string> Roles)
    : IRequest<Unit>;