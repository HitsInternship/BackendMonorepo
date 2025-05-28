package hits.internship.NotificationService.model.kafka;

import lombok.Data;
import lombok.EqualsAndHashCode;

@EqualsAndHashCode(callSuper = true)
@Data
public class Registration extends KafkaMessageRequest{
    String login;
    String password;
}