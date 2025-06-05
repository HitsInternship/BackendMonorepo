using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NotificationModule.Kafka.TopicInitializer;

public class KafkaTopicInitializer : IKafkaTopicInitializer
{
    private readonly KafkaSettings _settings;
    private readonly ILogger<KafkaTopicInitializer> _logger;

    public KafkaTopicInitializer(IOptions<KafkaSettings> settings, ILogger<KafkaTopicInitializer> logger)
    {
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task InitializeTopicsAsync()
    {
        var bootstrapServers = _settings.BootstrapServers;
        var topics = new List<string> { _settings.ConsumerTopic, _settings.ProducerTopic };

        using var adminClient =
            new AdminClientBuilder(new AdminClientConfig { BootstrapServers = bootstrapServers }).Build();

        foreach (var topic in topics)
        {
            try
            {
                await adminClient.CreateTopicsAsync([new TopicSpecification { Name = topic }]);
                _logger.LogInformation($"Topic '{topic}' created successfully.");
            }
            catch (CreateTopicsException e)
            {
                _logger.LogWarning(
                    $"An error occurred creating topic {e.Results[0].Topic}: {e.Results[0].Error.Reason}");
            }
        }
    }
}