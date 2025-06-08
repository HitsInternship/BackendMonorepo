using MediatR;
using SelectionModule.Contracts.Dtos.Requests;

namespace SelectionModule.Contracts.Commands.SelectionComment;

public record SendSelectionCommentToAllCommand(CommentRequestDto CommentRequestDto, Guid UserId) : IRequest<Unit>;