package hits.internship.NotificationService.model.output;

import lombok.Data;

import java.util.List;

@Data
public class TraceDto {
    private String traceId;
    private List<SpanDto> spans;
}

