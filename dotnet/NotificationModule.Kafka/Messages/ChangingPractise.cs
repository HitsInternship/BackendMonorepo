using NotificationModule.Domain.Enums;

namespace NotificationModule.Kafka.Messages;

public class ChangingPractise : KafkaMessageRequest
{
    public string OldCompanyName { get; set; }
    public string NewCompanyName { get; set; }
    public string OldPosition { get; set; }
    public string NewPosition { get; set; }
    public ChangingPracticeStatusType NewStatus { get; set; }
}