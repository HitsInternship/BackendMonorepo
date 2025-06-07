namespace NotificationModule.Kafka.TopicInitializer;

public interface IKafkaTopicInitializer
{
    Task InitializeTopicsAsync();
}