using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NotificationModule.Kafka;

public class MessageConsumer : BackgroundService
{
    private readonly IConsumer<Ignore, string> _consumer;
    private readonly ILogger<MessageConsumer> _logger;

    public MessageConsumer(IConsumer<Ignore, string> consumer, ILogger<MessageConsumer> logger)
    {
        _consumer = consumer;
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() =>
        {
            _consumer.Subscribe("notification.request");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var cr = _consumer.Consume(stoppingToken);
                    _logger.LogInformation($"Received message: {cr.Message.Value}");

                    // здесь вы можете десериализовать и обработать KafkaMessageRequest
                    // и отправить KafkaMessageResponse в другой топик, если нужно
                }
                catch (ConsumeException ex)
                {
                    _logger.LogError($"Consume error: {ex.Error.Reason}");
                }
            }

            _consumer.Close();
        }, stoppingToken);
    }
}