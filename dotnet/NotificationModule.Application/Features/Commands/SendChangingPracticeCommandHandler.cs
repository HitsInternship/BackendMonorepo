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

public class SendChangingPracticeCommandHandler : IRequestHandler<SendChangingPracticeCommand, Unit>
{
    private readonly IMessageProducer _messageProducer;
    private readonly IMessageRepository _messageRepository;

    public SendChangingPracticeCommandHandler(IMessageProducer messageProducer, IMessageRepository messageRepository)
    {
        _messageProducer = messageProducer;
        _messageRepository = messageRepository;
    }

    public async Task<Unit> Handle(SendChangingPracticeCommand request, CancellationToken cancellationToken)
    {
        var message = new ChangingPractise
        {
            Email = request.Email,
            EventType = EventType.changing_practise,
            OldCompanyName = request.OldCompanyName,
            NewCompanyName = request.NewCompanyName,
            OldPosition = request.OldPosition,
            NewPosition = request.NewPosition,
            NewStatus = ChangingPracticeStatusType.in_progress
        };

        var messageEntity = new Message
        {
            Id = message.Id,
            Email = message.Email,
            EventType = EventType.changing_practise,
            Data = JsonHelper.Serialize(message),
            MessageStatus = MessageStatus.in_progress
        };

        await _messageRepository.AddAsync(messageEntity);

        await _messageProducer.ProduceAsync(messageEntity.Data);

        return Unit.Value;
    }
}