using MediatR;
using SelectionModule.Contracts.Commands.SelectionComment;
using SelectionModule.Contracts.Repositories;
using Shared.Domain.Exceptions;

namespace SelectionModule.Application.Features.Commands.SelectionComment;

public class UpdateSelectionCommentCommandHandler : IRequestHandler<UpdateSelectionCommentCommand, Unit>
{
    private readonly ISelectionRepository _selectionRepository;
    private readonly ISelectionCommentRepository _selectionCommentRepository;

    public UpdateSelectionCommentCommandHandler(ISelectionRepository selectionRepository,
        ISelectionCommentRepository selectionCommentRepository)
    {
        _selectionRepository = selectionRepository;
        _selectionCommentRepository = selectionCommentRepository;
    }

    public async Task<Unit> Handle(UpdateSelectionCommentCommand request, CancellationToken cancellationToken)
    {
        if (!await _selectionRepository.CheckIfExistsAsync(request.SelectionId))
            throw new NotFound("Selection not found");

        if (!await _selectionCommentRepository.CheckIfExistsAsync(request.CommentId))
            throw new NotFound("Selection not found");

        var comment = await _selectionCommentRepository.GetByIdAsync(request.CommentId);

        if (comment.UserId != request.UserId)
            throw new Forbidden("You do not have access to leave comment");

        comment.Content = request.Comment;

        await _selectionCommentRepository.UpdateAsync(comment);

        return Unit.Value;
    }
}