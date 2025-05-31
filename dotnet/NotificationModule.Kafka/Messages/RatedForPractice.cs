namespace NotificationModule.Kafka.Messages;

public class RatedForPractice : KafkaMessageRequest
{
    public string Rate { get; set; }
    public Guid PractiseId { get; set; }
}