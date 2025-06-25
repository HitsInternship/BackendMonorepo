using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NotificationModule.Kafka.Consumers;

public class DisabledMessageConsumer : BackgroundService
{
    private readonly ILogger<DisabledMessageConsumer> _logger;

    public DisabledMessageConsumer(ILogger<DisabledMessageConsumer> logger)
    {
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogWarning("Kafka consumer is disabled");
        return Task.CompletedTask;
    }
}