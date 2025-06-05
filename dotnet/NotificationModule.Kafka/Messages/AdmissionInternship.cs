namespace NotificationModule.Kafka.Messages;

public class AdmissionInternship : KafkaMessageRequest
{
    public string CompanyName { get; set; }
    public string Position { get; set; }
}