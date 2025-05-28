using MediatR;
using SelectionModule.Contracts.Commands.SelectionComment;
using SelectionModule.Contracts.Repositories;
using SelectionModule.Domain.Entites;
using Shared.Domain.Exceptions;

namespace SelectionModule.Application.Features.Commands.SelectionComment;

public class CreateSelectionCommentCommandHandler : IRequestHandler<CreateSelectionCommentCommand, Unit>
{
    private readonly ISelectionRepository _selectionRepository;
    private readonly ISelectionCommentRepository _selectionCommentRepository;

    public CreateSelectionCommentCommandHandler(ISelectionRepository selectionRepository,
        ISelectionCommentRepository selectionCommentRepository)
    {
        _selectionRepository = selectionRepository;
        _selectionCommentRepository = selectionCommentRepository;
    }


    public async Task<Unit> Handle(CreateSelectionCommentCommand request, CancellationToken cancellationToken)
    {
        if (!await _selectionRepository.CheckIfExistsAsync(request.SelectionId))
            throw new NotFound("Selection not found");

        var selection = await _selectionRepository.GetByIdAsync(request.SelectionId);

        if (selection.Candidate.UserId != request.UserId && !request.Roles.Contains("DeanMember"))
            throw new Forbidden("You do not have access to leave comment");

        await _selectionCommentRepository.AddAsync(new SelectionCommentEntity
        {
            Content = request.Comment,
            UserId = request.UserId,
            ParentId = selection.Id,
            Selection = selection
        });

        return Unit.Value;
    }
}