using MediatR;
using Microsoft.EntityFrameworkCore;
using NotificationModule.Contracts.Commands;
using NotificationModule.Domain.Enums;
using SelectionModule.Contracts.Commands.SelectionComment;
using SelectionModule.Contracts.Repositories;
using SelectionModule.Domain.Entites;
using UserModule.Contracts.Repositories;

namespace SelectionModule.Application.Features.Commands.SelectionComment;

public class SendSelectionCommentToAllCommandHandler : IRequestHandler<SendSelectionCommentToAllCommand, Unit>
{
    private readonly ISender _sender;
    private readonly IUserRepository _userRepository;
    private readonly ISelectionRepository _selectionRepository;
    private readonly ISelectionCommentRepository _selectionCommentRepository;

    public SendSelectionCommentToAllCommandHandler(ISender sender, ISelectionRepository selectionRepository,
        IUserRepository userRepository, ISelectionCommentRepository selectionCommentRepository)
    {
        _sender = sender;
        _selectionRepository = selectionRepository;
        _userRepository = userRepository;
        _selectionCommentRepository = selectionCommentRepository;
    }

    public async Task<Unit> Handle(SendSelectionCommentToAllCommand request, CancellationToken cancellationToken)
    {
        var selections = await _selectionRepository.ListAllActiveAsync();

        if (request.CommentRequestDto.SelectedSelections != null && request.CommentRequestDto.SelectedSelections.Any())
        {
            selections = selections.Where(x =>
                x.Candidate.Selection != null &&
                request.CommentRequestDto.SelectedSelections.Contains(x.Candidate.Selection.Id));
        }
        else if (request.CommentRequestDto.SelectionStatus.HasValue)
            selections = selections.Where(x => x.SelectionStatus == request.CommentRequestDto.SelectionStatus.Value);

        var selectionsEntity = selections.Include(x => x.Candidate).ToList();

        foreach (var selection in selectionsEntity)
        {
            await _selectionCommentRepository.AddAsync(new SelectionCommentEntity
            {
                Content = request.CommentRequestDto.Content,
                UserId = request.UserId,
                ParentId = selection.Id,
                Selection = selection
            });

            var userToSend = await _userRepository.GetByIdAsync(selection.Candidate.UserId);

            var user = await _userRepository.GetByIdAsync(request.UserId);

            await _sender.Send(new SendNewCommentMessageCommand(userToSend.Email, CommentType.selection,
                request.CommentRequestDto.Content, user.Surname + " " + user.Surname, selection.Id), cancellationToken);
        }

        return Unit.Value;
    }
}