package hits.internship.NotificationService.service;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import hits.internship.NotificationService.config.KafkaProducer;
import hits.internship.NotificationService.model.enumeration.EventType;
import hits.internship.NotificationService.model.enumeration.KafkaMessageStatus;
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

    public void parsingGeneral(String message) {
        try{
            JsonNode rootNode = objectMapper.readTree(message);
            EventType eventType = EventType.valueOf(rootNode.get("eventType").asText());
            switch (eventType) {
                case deadline -> parsingDeadline(message);
                case registration -> parsingRegistration(message);
                case changing_password -> parsingChangingPassword(message);
                case changing_practise -> parsingChangingPractise(message);
                case admission_internship -> parsingAdmissionInternship(message);
                case rated_for_practice -> parsingRatedForPractice(message);
                case new_comment -> parsingNewComment(message);
                case meeting -> parsingMeeting(message);
                default -> sendError(message);
            }
        } catch (Exception ex) {
            log.error("Failed to extract eventType from malformed message: {}", message);
            sendError(message);
        }
    }

    private void parsingMeeting(String message) {
        try {
            Meeting meeting = objectMapper.readValue(message, Meeting.class);
            emailService.createMeeting(meeting);
        } catch (JsonProcessingException e) {
            log.error("Error parsing the message: {}", message);
            sendError(message);
            throw new RuntimeException(e);
        }
    }

    private void parsingNewComment(String message) {
        try {
            NewComment newComment = objectMapper.readValue(message, NewComment.class);
            emailService.createNewComment(newComment);
        } catch (JsonProcessingException e) {
            log.error("Error parsing the message: {}", message);
            sendError(message);
            throw new RuntimeException(e);
        }
    }

    private void parsingRatedForPractice(String message) {
        try {
            RatedForPractice ratedForPractice = objectMapper.readValue(message, RatedForPractice.class);
            emailService.createRatedForPractice(ratedForPractice);
        } catch (JsonProcessingException e) {
            log.error("Error parsing the message: {}", message);
            sendError(message);
            throw new RuntimeException(e);
        }
    }

    private void parsingAdmissionInternship(String message) {
        try {
            AdmissionInternship admissionInternship = objectMapper.readValue(message, AdmissionInternship.class);
            emailService.createAdmissionInternship(admissionInternship);
        } catch (JsonProcessingException e) {
            log.error("Error parsing the message: {}", message);
            sendError(message);
            throw new RuntimeException(e);
        }
    }

    public void parsingChangingPractise(String message) {
        try {
            ChangingPractise changingPractise = objectMapper.readValue(message, ChangingPractise.class);
            emailService.createChangePractise(changingPractise);
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
            emailService.createRegistrationMail(registration);
        } catch (JsonProcessingException e) {
            log.error("Error parsing the message: {}", message);
            sendError(message);
            throw new RuntimeException(e);
        }
    }

    public void parsingChangingPassword(String message) {
        try {
            ChangingPassword changingPassword = objectMapper.readValue(message, ChangingPassword.class);
            emailService.createChangingPassword(changingPassword);
        } catch (JsonProcessingException e) {
            log.error("Error parsing the message: {}", message);
            sendError(message);
            throw new RuntimeException(e);
        }
    }

    public void parsingDeadline(String message) {
        try {
            Deadline deadline = objectMapper.readValue(message, Deadline.class);
            emailService.createDeadlineMail(deadline);
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
            KafkaMessageResponse errorMessage = new KafkaMessageResponse(id, KafkaMessageStatus.error, "Error parsing the message");
            kafkaProducer.sendMessage(responseTopic, errorMessage.toString());
        } catch (Exception ex) {
            log.error("Failed to extract ID from malformed message: {}", message);
        }
    }
}
