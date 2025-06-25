using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationModule.Kafka.TopicInitializer;

namespace NotificationModule.Kafka;

public static class DependencyInjection
{
    public static void AddKafka(this IServiceCollection services, IConfiguration configuration)
    {
        var kafkaSection = configuration.GetSection("Kafka");

        if (!kafkaSection.Exists())
            throw new ArgumentException("Kafka configuration section is missing");

        var kafkaSettings = kafkaSection.Get<KafkaSettings>()
                            ?? throw new ArgumentException("Kafka settings are invalid");

        services.Configure<KafkaSettings>(kafkaSection);

        services.TryAddKafkaSafe(kafkaSettings);
    }


    public static async Task UseKafka(this IServiceProvider services)
    {
        var scope = services.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<IKafkaTopicInitializer>();
        await initializer.InitializeTopicsAsync();
    }
}