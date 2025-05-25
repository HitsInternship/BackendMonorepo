package hits.internship.NotificationService.model.kafka;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import hits.internship.NotificationService.model.enumeration.KafkaMessageStatus;
import hits.internship.NotificationService.model.enumeration.StatusType;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.RequiredArgsConstructor;

import java.util.HashMap;
import java.util.Map;
import java.util.UUID;
@Data
@AllArgsConstructor
public class KafkaMessageResponse {
    UUID id;
    KafkaMessageStatus status;
    String errorMessage;

    @Override
    public String toString() {
        ObjectMapper mapper = new ObjectMapper();
        Map<String, Object> jsonmap = new HashMap<>();
        jsonmap.put("id", id.toString());
        jsonmap.put("status", status.toString());
        jsonmap.put("errorMessage", errorMessage);

        try {
            return mapper.writeValueAsString(jsonmap);
        } catch (JsonProcessingException e) {
            throw new RuntimeException(e);
        }
    }
}
