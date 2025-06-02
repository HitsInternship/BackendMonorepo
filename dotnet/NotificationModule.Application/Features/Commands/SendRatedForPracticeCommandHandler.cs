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

public class SendRatedForPracticeCommandHandler : IRequestHandler<SendRatedForPracticeMessageCommand, Unit>
{
    private readonly IMessageProducer _messageProducer;
    private readonly IMessageRepository _messageRepository;

    public SendRatedForPracticeCommandHandler(IMessageProducer messageProducer, IMessageRepository messageRepository)
    {
        _messageProducer = messageProducer;
        _messageRepository = messageRepository;
    }

    public async Task<Unit> Handle(SendRatedForPracticeMessageCommand request, CancellationToken cancellationToken)
    {
        var message = new RatedForPractice
        {
            Email = request.Email,
            EventType = EventType.rated_for_practise,
            Rate = request.Rate,
            PractiseId = request.PractiseId
        };

        var messageEntity = new Message
        {
            Id = message.Id,
            Email = message.Email,
            EventType = EventType.rated_for_practise,
            Data = JsonHelper.Serialize(message),
            MessageStatus = MessageStatus.in_progress
        };

        await _messageRepository.AddAsync(messageEntity);

        await _messageProducer.ProduceAsync(messageEntity.Data);

        return Unit.Value;
    }
}