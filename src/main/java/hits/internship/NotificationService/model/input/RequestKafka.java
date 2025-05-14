package hits.internship.NotificationService.model.input;

import hits.internship.NotificationService.model.enumeration.EventTypeRole;

public record RequestKafka(
        String service_name,
        EventTypeRole event_type,
        String trace_id,
        String parent_span_id,
        String span_id,
        //@JsonFormat(shape = JsonFormat.Shape.STRING, pattern = "yyyy-MM-dd'T'HH:mm:ss'Z'")
        String timestamp,
        String log_message,
        DataKafka data
) {
}
