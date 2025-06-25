using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NotificationModule.Contracts.Kafka;
using NotificationModule.Kafka.Consumers;
using NotificationModule.Kafka.Producers;
using NotificationModule.Kafka.TopicInitializer;

namespace NotificationModule.Kafka;

public static class KafkaSafeRegistration
{
    public static void TryAddKafkaSafe(this IServiceCollection services, KafkaSettings kafkaSettings)
    {
        if (!kafkaSettings.KafkaEnabled)
        {
            services.AddSingleton<IMessageProducer, DisabledMessageProducer>();
            services.AddTransient<IKafkaTopicInitializer, DisabledKafkaTopicInitializer>();
            return;
        }

        services.AddSingleton<IProducer<Null, string>>(provider =>
        {
            var logger = provider.GetRequiredService<ILogger<ProducerBuilder<Null, string>>>();

            var config = new ProducerConfig
            {
                BootstrapServers = kafkaSettings.BootstrapServers
            };

            return new ProducerBuilder<Null, string>(config)
                .WithLogging(logger)
                .Build();
        });

        services.AddSingleton<IConsumer<Ignore, string>>(provider =>
        {
            var logger = provider.GetRequiredService<ILogger<ConsumerBuilder<Ignore, string>>>();

            var config = new ConsumerConfig
            {
                BootstrapServers = kafkaSettings.BootstrapServers,
                GroupId = kafkaSettings.GroupId,
                AutoOffsetReset =
                    Enum.TryParse<AutoOffsetReset>(kafkaSettings.AutoOffsetReset, true, out var offset)
                        ? offset
                        : AutoOffsetReset.Latest
            };

            return new ConsumerBuilder<Ignore, string>(config)
                .WithLogging(logger)
                .Build();
        });

        services.AddHostedService<MessageConsumer>();
        services.AddSingleton<IMessageProducer, MessageProducer>();
        services.AddTransient<IKafkaTopicInitializer, KafkaTopicInitializer>();
    }
}
