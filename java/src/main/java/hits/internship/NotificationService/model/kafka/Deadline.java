package hits.internship.NotificationService.model.kafka;

import hits.internship.NotificationService.model.enumeration.DeadlineType;
import lombok.Data;
import lombok.EqualsAndHashCode;

import java.time.LocalDate;
import java.time.OffsetDateTime;
@EqualsAndHashCode(callSuper = true)
@Data
public class Deadline extends KafkaMessageRequest{
    LocalDate deadlineDate;
    DeadlineType event;
}
