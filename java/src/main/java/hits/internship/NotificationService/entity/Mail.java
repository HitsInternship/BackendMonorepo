package hits.internship.NotificationService.entity;

import lombok.AllArgsConstructor;
import lombok.Data;

import java.util.UUID;

@Data
@AllArgsConstructor
public class Mail {
    private UUID id;
    private String to;
    private String subject;
    private String body;
}
