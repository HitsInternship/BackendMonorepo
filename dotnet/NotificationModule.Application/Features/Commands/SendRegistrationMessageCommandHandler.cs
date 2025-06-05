using MediatR;
using NotificationModule.Application.Helpers;
using NotificationModule.Contracts.Commands;
using NotificationModule.Contracts.Kafka;
using NotificationModule.Contracts.Repositories;
using NotificationModule.Domain.Entites;
using NotificationModule.Domain.Enums;
using NotificationModule.Kafka.Messages;

namespace NotificationModule.Application.Features.Commands;

public class SendRegistrationMessageCommandHandler : IRequestHandler<SendRegistrationMessageCommand, Unit>
{
    private readonly IMessageProducer _messageProducer;
    private readonly IMessageRepository _messageRepository;

    public SendRegistrationMessageCommandHandler(IMessageProducer messageProducer, IMessageRepository messageRepository)
    {
        _messageProducer = messageProducer;
        _messageRepository = messageRepository;
    }

    public async Task<Unit> Handle(SendRegistrationMessageCommand request, CancellationToken cancellationToken)
    {
        var registrationMessage = new Registration
        {
            Email = request.Email,
            EventType = EventType.registration,
            Password = request.Password
        };

        var message = new Message
        {
            Id = registrationMessage.Id,
            Email = request.Email,
            EventType = EventType.registration,
            Data = JsonHelper.Serialize(registrationMessage),
            MessageStatus = MessageStatus.in_progress
        };

        await _messageRepository.AddAsync(message);

        await _messageProducer.ProduceAsync(message.Data);

        return Unit.Value;
    }
}