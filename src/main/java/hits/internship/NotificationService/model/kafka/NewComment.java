package hits.internship.NotificationService.model.kafka;

import hits.internship.NotificationService.model.enumeration.CommentType;
import lombok.Data;
import lombok.EqualsAndHashCode;

import java.util.UUID;

@EqualsAndHashCode(callSuper = true)
@Data
public class NewComment extends KafkaMessageRequest{
    CommentType commentType;
    String message;
    String fullName;
    UUID typeId;
}
