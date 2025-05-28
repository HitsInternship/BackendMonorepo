package hits.internship.NotificationService.model.kafka;

import hits.internship.NotificationService.model.enumeration.StatusType;
import lombok.Data;
import lombok.EqualsAndHashCode;

@EqualsAndHashCode(callSuper = true)
@Data
public class ChangingPractise extends KafkaMessageRequest{
    String oldCompanyName;
    String oldPosition;
    String newCompanyName;
    String newPosition;
    StatusType newStatus;
}
