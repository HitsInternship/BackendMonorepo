using MediatR;
using NotificationModule.Contracts.Commands;
using NotificationModule.Domain.Enums;
using SelectionModule.Contracts.Commands.SelectionComment;
using SelectionModule.Contracts.Repositories;
using UserModule.Contracts.Repositories;

namespace SelectionModule.Application.Features.Commands.SelectionComment;

public class SendSelectionCommentToAllCommandHandler : IRequestHandler<SendSelectionCommentToAllCommand, Unit>
{
    private readonly ISender _sender;
    private readonly IUserRepository _userRepository;
    private readonly ISelectionRepository _selectionRepository;

    public SendSelectionCommentToAllCommandHandler(ISender sender, ISelectionRepository selectionRepository,
        IUserRepository userRepository)
    {
        _sender = sender;
        _selectionRepository = selectionRepository;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(SendSelectionCommentToAllCommand request, CancellationToken cancellationToken)
    {
        var selections = await _selectionRepository.ListAllActiveAsync();

        if (request.CommentRequestDto.SelectionStatus.HasValue)
            selections = selections.Where(x => x.SelectionStatus == request.CommentRequestDto.SelectionStatus.Value);

        var selectionsEntity = selections.ToList().AsReadOnly();

        foreach (var selection in selectionsEntity)
        {
            var userToSend = await _userRepository.GetByIdAsync(selection.Candidate.UserId);

            var user = await _userRepository.GetByIdAsync(request.UserId);

            await _sender.Send(new SendNewCommentMessageCommand(userToSend.Email, CommentType.selection,
                request.CommentRequestDto.Content, user.Surname + " " + user.Surname, selection.Id), cancellationToken);
        }

        return Unit.Value;
    }
}