package hits.internship.NotificationService.model.kafka;

import hits.internship.NotificationService.model.enumeration.StatusType;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.RequiredArgsConstructor;

import java.util.UUID;
@Data
@AllArgsConstructor
public class KafkaMessageResponse {
    UUID id;
    StatusType status;
    String errorMessage;
}
