using NotificationModule.Domain.Enums;

namespace NotificationModule.Kafka.Messages;

public class Deadline : KafkaMessageRequest 
{
    public DateOnly DeadlineDate { get; set; }
    public DeadLineType Event  { get; set; }
}