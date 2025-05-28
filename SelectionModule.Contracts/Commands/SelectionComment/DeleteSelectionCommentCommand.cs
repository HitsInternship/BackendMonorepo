using MediatR;

namespace SelectionModule.Contracts.Commands.SelectionComment;

public record DeleteSelectionCommentCommand(Guid SelectionId, Guid CommentId, Guid UserId) : IRequest<Unit>;