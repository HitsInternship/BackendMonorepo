using System.Text.Json;
using MediatR;
using NotificationModule.Application.Helpers;
using NotificationModule.Contracts.Commands;
using NotificationModule.Contracts.Kafka;
using NotificationModule.Contracts.Repositories;
using NotificationModule.Domain.Entites;
using NotificationModule.Domain.Enums;
using NotificationModule.Kafka.Messages;

namespace NotificationModule.Application.Features.Commands;

public class SendDeadlineMessageCommandHandler : IRequestHandler<SendDeadlineMessageCommand, Unit>
{
    private readonly IMessageProducer _messageProducer;
    private readonly IMessageRepository _messageRepository;

    public SendDeadlineMessageCommandHandler(IMessageProducer messageProducer, IMessageRepository messageRepository)
    {
        _messageProducer = messageProducer;
        _messageRepository = messageRepository;
    }

    public async Task<Unit> Handle(SendDeadlineMessageCommand request, CancellationToken cancellationToken)
    {
        var message = new Deadline
        {
            Email = request.Email,
            EventType = EventType.deadline,
            DeadlineDate = request.DeadLineDate,
            Event = request.EventType
        };

        var messageEntity = new Message
        {
            Id = message.Id,
            Email = message.Email,
            EventType = EventType.deadline,
            Data = JsonHelper.Serialize(message),
            MessageStatus = MessageStatus.in_progress
        };

        await _messageRepository.AddAsync(messageEntity);

        await _messageProducer.ProduceAsync(messageEntity.Data);

        return Unit.Value;
    }
}