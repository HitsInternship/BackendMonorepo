package hits.internship.NotificationService.config;

import hits.internship.NotificationService.service.NotificationService;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.kafka.annotation.KafkaListener;
import org.springframework.stereotype.Service;

@Service
@Slf4j
@RequiredArgsConstructor
public class KafkaConsumer {

    private final NotificationService notificationService;

    @KafkaListener(topics = "${notification.topic.request}")
    public void listen(String message) {
        log.info("Message received: {}", message);
        notificationService.parsingGeneral(message);
    }
}
