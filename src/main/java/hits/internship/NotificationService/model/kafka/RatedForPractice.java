package hits.internship.NotificationService.model.kafka;

import lombok.Data;
import lombok.EqualsAndHashCode;

import java.util.UUID;

@EqualsAndHashCode(callSuper = true)
@Data
public class RatedForPractice extends KafkaMessageRequest {
    String rate;
    UUID practiceId;
}
