package hits.internship.NotificationService.model.kafka;

import hits.internship.NotificationService.model.enumeration.EventType;
import lombok.Data;

import java.util.UUID;
@Data
public abstract class KafkaMessageRequest {
    private UUID id;
    private String email;
    private EventType eventType;
}
