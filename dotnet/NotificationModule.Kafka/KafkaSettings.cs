namespace NotificationModule.Kafka;

public class KafkaSettings
{
    public string BootstrapServers { get; set; }
    public string GroupId { get; set; }
    public string AutoOffsetReset { get; set; }
}
