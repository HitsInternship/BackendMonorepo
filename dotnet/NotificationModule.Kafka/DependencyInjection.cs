using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Confluent.Kafka;

namespace NotificationModule.Kafka;

public static class DependencyInjection
{
    public static void AddKafka(this IServiceCollection services, IConfiguration configuration)
    {
        var kafkaSettings = configuration.GetSection("Kafka").Get<KafkaSettings>() ?? throw new ArgumentException();

        services.AddSingleton<IProducer<Null, string>>(_ =>
        {
            var config = new ProducerConfig
            {
                BootstrapServers = kafkaSettings.BootstrapServers
            };
            return new ProducerBuilder<Null, string>(config).Build();
        });

        services.AddSingleton<IConsumer<Ignore, string>>(_ =>
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = kafkaSettings.BootstrapServers,
                GroupId = kafkaSettings.GroupId,
                AutoOffsetReset = Enum.TryParse<AutoOffsetReset>(kafkaSettings.AutoOffsetReset, true, out var offset)
                    ? offset
                    : AutoOffsetReset.Latest
            };
            return new ConsumerBuilder<Ignore, string>(config).Build();
        });

        services.AddHostedService<MessageConsumer>();
        services.AddSingleton<MessageProducer>();
    }
}