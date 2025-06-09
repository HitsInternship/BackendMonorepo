using MediatR;
using NotificationModule.Contracts.Commands;
using NotificationModule.Domain.Enums;
using SelectionModule.Contracts.Commands.SelectionComment;
using SelectionModule.Contracts.Repositories;
using SelectionModule.Domain.Entites;
using Shared.Domain.Exceptions;
using UserModule.Contracts.Repositories;

namespace SelectionModule.Application.Features.Commands.SelectionComment;

public class CreateSelectionCommentCommandHandler : IRequestHandler<CreateSelectionCommentCommand, Unit>
{
    private readonly ISender _sender;
    private readonly IUserRepository _userRepository;
    private readonly ISelectionRepository _selectionRepository;
    private readonly ISelectionCommentRepository _selectionCommentRepository;

    public CreateSelectionCommentCommandHandler(ISelectionRepository selectionRepository,
        ISelectionCommentRepository selectionCommentRepository, ISender sender, IUserRepository userRepository)
    {
        _selectionRepository = selectionRepository;
        _selectionCommentRepository = selectionCommentRepository;
        _sender = sender;
        _userRepository = userRepository;
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

        if (request.UserId != selection.Candidate.UserId)
        {
            var userToSend = await _userRepository.GetByIdAsync(selection.Candidate.UserId);

            var user = await _userRepository.GetByIdAsync(request.UserId);

            await _sender.Send(new SendNewCommentMessageCommand(userToSend.Email, CommentType.selection,
                request.Comment, user.Surname + " " + user.Surname, selection.Id), cancellationToken);
        }
        
        return Unit.Value;
    }
}