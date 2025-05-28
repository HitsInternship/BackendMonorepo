using MediatR;

namespace SelectionModule.Contracts.Commands.SelectionComment;

public record UpdateSelectionCommentCommand(Guid SelectionId, Guid CommentId, Guid UserId, string Comment)
    : IRequest<Unit>;