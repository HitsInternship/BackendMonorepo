package hits.internship.NotificationService.config;

import hits.internship.NotificationService.service.EmailService;
import hits.internship.NotificationService.service.NotificationService;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.boot.autoconfigure.condition.ConditionalOnProperty;
import org.springframework.kafka.annotation.KafkaListener;
import org.springframework.kafka.annotation.TopicPartition;
import org.springframework.stereotype.Service;

@Service
@Slf4j
@RequiredArgsConstructor
@ConditionalOnProperty(value = "${spring.kafka.enabled}", havingValue = "true")
public class KafkaConsumer {

    private final NotificationService notificationService;

    @KafkaListener(topicPartitions =@TopicPartition(
            topic = "${notification.topic.request}",
            partitions = "0"
    ))
    public void listenChangingPractise(String message) {
        log.info("Message received: {}", message);
        notificationService.parsingChangingPractise(message);
    }

    @KafkaListener(topicPartitions =@TopicPartition(
            topic = "${notification.topic.request}",
            partitions = "1"
    ))
    public void listenRegistration(String message) {
        log.info("Message received: {}", message);
        notificationService.parsingRegistration(message);
    }

    @KafkaListener(topicPartitions =@TopicPartition(
            topic = "${notification.topic.request}",
            partitions = "2"
    ))
    public void listenChangingPassword(String message) {
        log.info("Message received: {}", message);
        notificationService.parsingChangingPassword(message);
    }

    @KafkaListener(topicPartitions =@TopicPartition(
            topic = "${notification.topic.request}",
            partitions = "3"
    ))
    public void listenDeadline(String message) {
        log.info("Message received: {}", message);
        notificationService.parsingDeadline(message);
    }
}
