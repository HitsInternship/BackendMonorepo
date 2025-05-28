package hits.internship.NotificationService.model.kafka;

import lombok.Data;
import lombok.EqualsAndHashCode;

@EqualsAndHashCode(callSuper = true)
@Data
public class AdmissionInternship extends KafkaMessageRequest{
    String companyName;
    String position;
}
