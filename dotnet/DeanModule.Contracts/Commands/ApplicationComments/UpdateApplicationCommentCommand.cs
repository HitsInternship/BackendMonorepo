using MediatR;

namespace DeanModule.Contracts.Commands.ApplicationComments;

public record UpdateApplicationCommentCommand(
    Guid ApplicationId,
    Guid ApplicationCommentId,
    string Content,
    Guid UserId) : IRequest<Unit>;