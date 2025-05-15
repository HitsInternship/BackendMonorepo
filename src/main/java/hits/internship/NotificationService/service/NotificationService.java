package hits.internship.NotificationService.service;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import hits.internship.NotificationService.config.KafkaProducer;
import hits.internship.NotificationService.model.enumeration.StatusType;
import hits.internship.NotificationService.model.kafka.*;
import lombok.RequiredArgsConstructor;
import lombok.SneakyThrows;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
@Slf4j
@RequiredArgsConstructor
public class NotificationService {

    private final EmailService emailService;
    private final ObjectMapper objectMapper;
    private final KafkaProducer kafkaProducer;

    @Value("${notification.topic.response}")
    private String responseTopic;

    public void parsingChangingPractise(String message) {
        try {
            ChangingPractise changingPractise = objectMapper.readValue(message, ChangingPractise.class);
        } catch (JsonProcessingException e) {
            log.error("Error parsing the message: {}", message);
            sendError(message);
            throw new RuntimeException(e);
        }
    }

    @SneakyThrows
    public void parsingRegistration(String message) {
        try {
            Registration registration = objectMapper.readValue(message, Registration.class);
        } catch (JsonProcessingException e) {
            log.error("Error parsing the message: {}", message);
            sendError(message);
            throw new RuntimeException(e);
        }
    }

    public void parsingChangingPassword(String message) {
        try {
            ChangingPassword changingPassword = objectMapper.readValue(message, ChangingPassword.class);
        } catch (JsonProcessingException e) {
            log.error("Error parsing the message: {}", message);
            sendError(message);
            throw new RuntimeException(e);
        }
    }

    public void parsingDeadline(String message) {
        try {
            Deadline deadline = objectMapper.readValue(message, Deadline.class);
        } catch (JsonProcessingException e) {
            log.error("Error parsing the message: {}", message);
            sendError(message);
            throw new RuntimeException(e);
        }
    }

    public void sendError(String message) {
        try{
            JsonNode rootNode = objectMapper.readTree(message);
            UUID id = UUID.fromString(rootNode.get("id").asText());
            KafkaMessageResponse errorMessage = new KafkaMessageResponse(id, StatusType.denied, "Error parsing the message: " + message);
            kafkaProducer.sendMessage(responseTopic, errorMessage.toString());
        } catch (Exception ex) {
            log.error("Failed to extract ID from malformed message: {}", message);
        }
    }
}
