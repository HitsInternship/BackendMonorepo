using Microsoft.Extensions.Logging;

namespace NotificationModule.Kafka.TopicInitializer;

public class DisabledKafkaTopicInitializer : IKafkaTopicInitializer
{
    private readonly ILogger<DisabledKafkaTopicInitializer> _logger;

    public DisabledKafkaTopicInitializer(ILogger<DisabledKafkaTopicInitializer> logger)
    {
        _logger = logger;
    }

    public Task InitializeTopicsAsync()
    {
        _logger.LogWarning("Kafka disabled");

        return Task.CompletedTask;
    }
}