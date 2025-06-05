namespace NotificationModule.Kafka.Messages;

public class Registration : KafkaMessageRequest
{
    public string Password { get; set; }
}