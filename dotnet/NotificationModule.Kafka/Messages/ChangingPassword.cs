namespace NotificationModule.Kafka.Messages;

public class ChangingPassword : KafkaMessageRequest
{
    public string Code { get; set; }
}