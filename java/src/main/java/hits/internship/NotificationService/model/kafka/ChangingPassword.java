package hits.internship.NotificationService.model.kafka;

import lombok.Data;
import lombok.EqualsAndHashCode;

@EqualsAndHashCode(callSuper = true)
@Data
public class ChangingPassword extends KafkaMessageRequest{
    String code;
}
