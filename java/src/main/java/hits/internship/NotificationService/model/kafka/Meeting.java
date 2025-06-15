package hits.internship.NotificationService.model.kafka;

import lombok.Data;
import lombok.EqualsAndHashCode;

import java.time.LocalDateTime;
import java.util.UUID;

@EqualsAndHashCode(callSuper = true)
@Data
public class Meeting extends KafkaMessageRequest{
    UUID meetingId;
    String companyName;
    LocalDateTime meetingDateTime;
}
