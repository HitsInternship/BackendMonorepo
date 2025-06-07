namespace NotificationModule.Contracts.Kafka;

public interface IMessageProducer 
{
    Task ProduceAsync<T>(T message);
}