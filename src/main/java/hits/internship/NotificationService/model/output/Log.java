package hits.internship.NotificationService.model.output;

import java.time.LocalDateTime;

public record Log(
        String serviceName,
        String eventType,
        String logMessage,
        LocalDateTime timestamp
) {
}
