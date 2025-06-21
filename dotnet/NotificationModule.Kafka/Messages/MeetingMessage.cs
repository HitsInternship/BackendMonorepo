namespace NotificationModule.Kafka.Messages;

public class MeetingMessage : KafkaMessageRequest
{
    public required Guid MeetingId { get; set; }

    public required string CompanyName { get; set; }

    public required DateTime MeetingDateTime { get; set; }
}