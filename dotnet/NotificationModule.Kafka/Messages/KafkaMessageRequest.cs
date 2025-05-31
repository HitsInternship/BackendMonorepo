using NotificationModule.Domain.Enums;

namespace NotificationModule.Kafka.Messages;

public class KafkaMessageRequest
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public EventType EventType { get; set; }
}