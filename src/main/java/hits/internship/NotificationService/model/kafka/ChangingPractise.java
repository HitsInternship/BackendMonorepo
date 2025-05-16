package hits.internship.NotificationService.model.kafka;

import hits.internship.NotificationService.model.enumeration.StatusType;

public class ChangingPractise extends KafkaMessageRequest{
    String oldCompanyName;
    String oldPosition;
    String newCompanyName;
    String newPosition;
    StatusType newStatus;
}
