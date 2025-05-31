using NotificationModule.Domain.Enums;

namespace NotificationModule.Kafka.Messages;

public class KafkaMessageResponse
{
    public Guid Id {get;set;}
    public MessageStatus Status {get;set;}
    public string ErrorMessage {get;set;}
}