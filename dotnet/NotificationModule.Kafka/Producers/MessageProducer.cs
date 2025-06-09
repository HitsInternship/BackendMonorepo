using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NotificationModule.Contracts.Kafka;

namespace NotificationModule.Kafka.Producers;

public class MessageProducer : IMessageProducer
{
    private readonly IProducer<Null, string> _producer;
    private readonly ILogger<MessageProducer> _logger;
    private readonly string _topic;

    public MessageProducer(IProducer<Null, string> producer, ILogger<MessageProducer> logger,
        IOptions<KafkaSettings> kafkaSettings)
    {
        _producer = producer;
        _logger = logger;
        _topic = kafkaSettings.Value.ProducerTopic;
    }

    public async Task ProduceAsync<T>(T message)
    {
        var jsonMessage = JsonSerializer.Serialize(message);
        var result = await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = jsonMessage });
        _logger.LogInformation($"Message sent to topic {_topic}, offset: {result.Offset}");
    }
}