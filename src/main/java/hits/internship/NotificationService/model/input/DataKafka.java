package hits.internship.NotificationService.model.input;

public record DataKafka(
        String http_method,
        String url
) {
}
