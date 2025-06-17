using MediatR;
using NotificationModule.Application.Helpers;
using NotificationModule.Contracts.Commands;
using NotificationModule.Contracts.Kafka;
using NotificationModule.Contracts.Repositories;
using NotificationModule.Domain.Entites;
using NotificationModule.Domain.Enums;
using NotificationModule.Kafka.Messages;

namespace NotificationModule.Application.Features.Commands;

public class SendMeetingMessageCommandHandler : IRequestHandler<SendMeetingMessageCommand, Unit>
{
    private readonly IMessageProducer _messageProducer;
    private readonly IMessageRepository _messageRepository;

    public SendMeetingMessageCommandHandler(IMessageProducer messageProducer, IMessageRepository messageRepository)
    {
        _messageProducer = messageProducer;
        _messageRepository = messageRepository;
    }

    public async Task<Unit> Handle(SendMeetingMessageCommand request, CancellationToken cancellationToken)
    {
        foreach (var email in request.Emails)
        {
            var message = new MeetingMessage
            {
                Email = email,
                EventType = EventType.meeting,
                MeetingId = request.AppointmentId,
                CompanyName = request.CompanyName,
                MeetingDateTime = request.DateTime
            };

            var messageEntity = new Message
            {
                Id = message.Id,
                Email = email,
                EventType = EventType.meeting,
                Data = JsonHelper.Serialize(message),
                MessageStatus = MessageStatus.in_progress
            };

            await _messageRepository.AddAsync(messageEntity);

            await _messageProducer.ProduceAsync(messageEntity.Data);
        }

        return Unit.Value;
    }
}