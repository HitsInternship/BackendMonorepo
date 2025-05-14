package hits.internship.NotificationService.service;

import hits.internship.NotificationService.entity.Token;
import hits.internship.NotificationService.model.input.DeletedTokenKafka;
import hits.internship.NotificationService.repository.TokenRepository;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import lombok.RequiredArgsConstructor;
import lombok.SneakyThrows;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Service;

@Service
@Slf4j
@RequiredArgsConstructor
public class TokenService {
    private final TokenRepository tokenRepository;
    @SneakyThrows
    public void addDeletedToken(String message) {
        try {
            ObjectMapper objectMapper = new ObjectMapper();
            DeletedTokenKafka deletedTokenKafka = objectMapper.readValue(message, DeletedTokenKafka.class);
            Token token = new Token(deletedTokenKafka.deleted_token());
            tokenRepository.save(token);
        } catch (JsonProcessingException e) {
            log.error("Error parsing the message: {}", message);
            throw new RuntimeException(e);
        }
    }
}
