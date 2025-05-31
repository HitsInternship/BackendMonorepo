using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Confluent.Kafka;
using NotificationModule.Contracts.Kafka;
using NotificationModule.Kafka.TopicInitializer;

namespace NotificationModule.Kafka;

public static class DependencyInjection
{
    public static void AddKafka(this IServiceCollection services, IConfiguration configuration)
    {
        var kafkaSettings = configuration.GetSection("Kafka").Get<KafkaSettings>() ?? throw new ArgumentException();

        services.Configure<KafkaSettings>(configuration.GetSection("Kafka"));

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
        services.AddSingleton<IMessageProducer, MessageProducer>();

        services.AddTransient<IKafkaTopicInitializer, KafkaTopicInitializer>();
    }

    public static async Task UseKafka(this IServiceProvider services)
    {
        var scope = services.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<IKafkaTopicInitializer>();
        await initializer.InitializeTopicsAsync();
    }
}