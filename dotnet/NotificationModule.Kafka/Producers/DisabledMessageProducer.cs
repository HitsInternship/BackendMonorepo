using Microsoft.Extensions.Logging;
using NotificationModule.Contracts.Kafka;

namespace NotificationModule.Kafka.Producers;

public class DisabledMessageProducer : IMessageProducer
{
    private readonly ILogger<DisabledMessageProducer> _logger;

    public DisabledMessageProducer(ILogger<DisabledMessageProducer> logger)
    {
        _logger = logger;
    }

    public Task ProduceAsync<T>(T message)
    {
        _logger.LogWarning("Kafka producer is disabled");

        return Task.CompletedTask;
    }
}