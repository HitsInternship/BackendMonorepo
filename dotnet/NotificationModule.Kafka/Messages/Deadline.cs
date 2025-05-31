using NotificationModule.Domain.Enums;

namespace NotificationModule.Kafka.Messages;

public class Deadline : KafkaMessageRequest 
{
    public DateTime DeadlineDate { get; set; }
    public DeadLineType Event  { get; set; }
}