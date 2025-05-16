package hits.internship.NotificationService.model.kafka;

import lombok.Data;
public class Registration extends KafkaMessageRequest{
    String login;
    String password;

}