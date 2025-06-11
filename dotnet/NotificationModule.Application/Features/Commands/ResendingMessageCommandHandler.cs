using MediatR;
using NotificationModule.Contracts.Commands;
using NotificationModule.Contracts.Kafka;
using NotificationModule.Contracts.Repositories;
using NotificationModule.Domain.Enums;

namespace NotificationModule.Application.Features.Commands;

public class ResendingMessageCommandHandler : IRequestHandler<ResendMessageCommand, Unit>
{
    private readonly IMessageProducer _messageProducer;
    private readonly IMessageRepository _messageRepository;

    public ResendingMessageCommandHandler(IMessageProducer messageProducer, IMessageRepository messageRepository)
    {
        _messageProducer = messageProducer;
        _messageRepository = messageRepository;
    }

    public async Task<Unit> Handle(ResendMessageCommand request, CancellationToken cancellationToken)
    {
        await _messageProducer.ProduceAsync(request.Message.Data);

        request.Message.MessageStatus = MessageStatus.in_progress;

        await _messageRepository.UpdateAsync(request.Message);

        return Unit.Value;
    }
}