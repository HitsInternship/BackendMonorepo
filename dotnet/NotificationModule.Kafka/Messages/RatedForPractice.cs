namespace NotificationModule.Kafka.Messages;

public class RatedForPractice : KafkaMessageRequest
{
    public string Rate { get; set; }
    public Guid PracticeId { get; set; }
}