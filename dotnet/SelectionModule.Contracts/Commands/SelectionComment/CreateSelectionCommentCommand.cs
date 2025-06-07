using MediatR;

namespace SelectionModule.Contracts.Commands.SelectionComment;

public record CreateSelectionCommentCommand(Guid SelectionId, Guid UserId, string Comment, List<string> Roles) : IRequest<Unit>;