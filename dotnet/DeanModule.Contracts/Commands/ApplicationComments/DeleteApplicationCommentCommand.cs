using MediatR;

namespace DeanModule.Contracts.Commands.ApplicationComments;

public record DeleteApplicationCommentCommand(Guid ApplicationId, Guid ApplicationCommentId, Guid UserId)
    : IRequest<Unit>;