using NotificationModule.Domain.Enums;

namespace NotificationModule.Kafka.Messages;

public class NewComment : KafkaMessageRequest
{
    public CommentType CommentType { get; set; }
    public string Message { get; set; }
    public string FullName { get; set; }
    public Guid Typeid { get; set; }
}