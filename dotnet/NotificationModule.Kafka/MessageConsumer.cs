using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NotificationModule.Contracts.Commands;
using NotificationModule.Kafka.Messages;

namespace NotificationModule.Kafka;

public class MessageConsumer : BackgroundService
{
    private readonly IConsumer<Ignore, string> _consumer;
    private readonly ILogger<MessageConsumer> _logger;
    private readonly ISender _sender;
    private readonly string _topic;

    public MessageConsumer(IConsumer<Ignore, string> consumer, ILogger<MessageConsumer> logger, ISender sender,
        IOptions<KafkaSettings> kafkaSettings)
    {
        _consumer = consumer;
        _logger = logger;
        _sender = sender;
        _topic = kafkaSettings.Value.ConsumerTopic;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(async () =>
        {
            _consumer.Subscribe(_topic);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var cr = _consumer.Consume(stoppingToken);
                    _logger.LogInformation($"Received message: {cr.Message.Value}");

                    var response =
                        JsonConvert.DeserializeObject<KafkaMessageResponse>(cr.Message.Value);

                    if (response != null)
                    {
                        if (response.ErrorMessage != null)
                        {
                            _logger.LogError(response.ErrorMessage);
                        }

                        await _sender.Send(new UpdateMessageStatusCommand(response.Id, response.Status), stoppingToken);
                    }
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