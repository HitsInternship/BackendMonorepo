using MediatR;
using NotificationModule.Contracts.Commands;
using NotificationModule.Contracts.Kafka;

namespace NotificationModule.Application.Features.Commands;

public class ResendingMessageCommandHandler : IRequestHandler<ResendMessageCommand, Unit>
{
    private readonly IMessageProducer _messageProducer;

    public ResendingMessageCommandHandler(IMessageProducer messageProducer)
    {
        _messageProducer = messageProducer;
    }

    public async Task<Unit> Handle(ResendMessageCommand request, CancellationToken cancellationToken)
    {
        await _messageProducer.ProduceAsync(request.Message.Data);

        return Unit.Value;
    }
}