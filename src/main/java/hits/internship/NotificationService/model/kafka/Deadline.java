package hits.internship.NotificationService.model.kafka;

import hits.internship.NotificationService.model.enumeration.DeadlineType;

import java.time.OffsetDateTime;

public class Deadline extends KafkaMessageRequest{
    OffsetDateTime deadlineDate;
    DeadlineType event;
}
