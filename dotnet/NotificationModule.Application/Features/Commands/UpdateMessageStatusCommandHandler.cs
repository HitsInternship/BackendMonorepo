using MediatR;
using NotificationModule.Contracts.Commands;
using NotificationModule.Contracts.Repositories;

namespace NotificationModule.Application.Features.Commands;

public class UpdateMessageStatusCommandHandler : IRequestHandler<UpdateMessageStatusCommand, Unit>
{
    private readonly IMessageRepository _messageRepository;

    public UpdateMessageStatusCommandHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<Unit> Handle(UpdateMessageStatusCommand request, CancellationToken cancellationToken)
    {
        var message = await _messageRepository.GetByIdAsync(request.MessageId);

        message.MessageStatus = request.Status;

        await _messageRepository.UpdateAsync(message);

        return Unit.Value;
    }
}