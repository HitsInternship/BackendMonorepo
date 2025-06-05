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

public class SendNewCommentMessageCommandHandler : IRequestHandler<SendNewCommentMessageCommand, Unit>
{
    private readonly IMessageProducer _messageProducer;
    private readonly IMessageRepository _messageRepository;

    public SendNewCommentMessageCommandHandler(IMessageProducer messageProducer, IMessageRepository messageRepository)
    {
        _messageProducer = messageProducer;
        _messageRepository = messageRepository;
    }

    public async Task<Unit> Handle(SendNewCommentMessageCommand request, CancellationToken cancellationToken)
    {
        var message = new NewComment
        {
            Email = request.Email,
            EventType = EventType.new_comment,
            CommentType = request.CommentType,
            Message = request.Message,
            FullName = request.Fullname,
            Typeid = request.Typeid
        };

        var messageEntity = new Message
        {
            Id = message.Id,
            Email = message.Email,
            EventType = EventType.new_comment,
            Data = JsonHelper.Serialize(message),
            MessageStatus = MessageStatus.in_progress
        };

        await _messageRepository.AddAsync(messageEntity);

        await _messageProducer.ProduceAsync(messageEntity.Data);

        return Unit.Value;
    }
}