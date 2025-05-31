using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace NotificationModule.Kafka;

public class MessageProducer
{
    private readonly IProducer<Null, string> _producer;
    private readonly ILogger<MessageProducer> _logger;

    public MessageProducer(IProducer<Null, string> producer)
    {
        _producer = producer;
    }

    public async Task ProduceAsync<T>(string topic, T message)
    {
        var jsonMessage = JsonSerializer.Serialize(message);
        var result = await _producer.ProduceAsync(topic, new Message<Null, string> { Value = jsonMessage });
        _logger.LogInformation($"Message sent to topic {topic}, offset: {result.Offset}");
    }
}
