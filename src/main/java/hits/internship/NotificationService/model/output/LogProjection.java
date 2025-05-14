package hits.internship.NotificationService.model.output;

import hits.internship.NotificationService.model.enumeration.EventTypeRole;

import java.time.OffsetDateTime;

public record LogProjection(
    String serviceName,
    EventTypeRole eventType,
    String logMessage,
    OffsetDateTime timestamp
) {}
