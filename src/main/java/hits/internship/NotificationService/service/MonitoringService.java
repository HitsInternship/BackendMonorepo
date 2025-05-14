package hits.internship.NotificationService.service;

import hits.internship.NotificationService.config.JwtTokenProvider;
import hits.internship.NotificationService.entity.ErrorLog;
import hits.internship.NotificationService.entity.RequestLog;
import hits.internship.NotificationService.entity.ResponseLog;
import hits.internship.NotificationService.exception.UnauthorizedException;
import hits.internship.NotificationService.model.input.ErrorKafka;
import hits.internship.NotificationService.model.input.RequestKafka;
import hits.internship.NotificationService.model.input.ResponseKafka;
import hits.internship.NotificationService.model.output.LogProjection;
import hits.internship.NotificationService.model.output.SpanDto;
import hits.internship.NotificationService.model.output.TraceDto;
import hits.internship.NotificationService.model.output.TraceInfoDto;
import hits.internship.NotificationService.repository.ErrorLogRepository;
import hits.internship.NotificationService.repository.RequestLogRepository;
import hits.internship.NotificationService.repository.ResponseLogRepository;
import hits.internship.NotificationService.repository.TokenRepository;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import lombok.RequiredArgsConstructor;
import lombok.SneakyThrows;
import lombok.extern.slf4j.Slf4j;
import org.springframework.security.core.Authentication;
import org.springframework.stereotype.Service;

import java.time.OffsetDateTime;
import java.util.*;
import java.util.stream.Collectors;

@Service
@Slf4j
@RequiredArgsConstructor
public class MonitoringService {

    private final ErrorLogRepository errorLogRepository;
    private final RequestLogRepository requestLogRepository;
    private final ResponseLogRepository responseLogRepository;
    private final JwtTokenProvider jwtTokenProvider;
    private final TokenRepository tokenRepository;
    private final ObjectMapper objectMapper;

    @SneakyThrows
    public void parsingMessageRequest(String message) {
        try {
            RequestKafka requestKafka = objectMapper.readValue(message, RequestKafka.class);
            OffsetDateTime offsetDateTime = OffsetDateTime.parse(requestKafka.timestamp());
            RequestLog requestLog = new RequestLog(
                    UUID.randomUUID(),
                    requestKafka.service_name(),
                    requestKafka.event_type(),
                    requestKafka.trace_id(),
                    requestKafka.parent_span_id(),
                    requestKafka.span_id(),
                    offsetDateTime, // явный парсинг
                    requestKafka.log_message(),
                    requestKafka.data().http_method(),
                    requestKafka.data().url()
            );
            requestLogRepository.save(requestLog);
        } catch (Exception e) {
            log.error("Error parsing message: {}, error: {}", message, e.getMessage());
            throw e;
        }
    }
    @SneakyThrows
    public void parsingMessageResponse(String message) {
        try {
            ObjectMapper objectMapper = new ObjectMapper();
            ResponseKafka responseKafka = objectMapper.readValue(message, ResponseKafka.class);
            OffsetDateTime offsetDateTime = OffsetDateTime.parse(responseKafka.timestamp());
            ResponseLog responseLog = new ResponseLog(
                    UUID.randomUUID(),
                    responseKafka.service_name(),
                    responseKafka.event_type(),
                    responseKafka.trace_id(),
                    responseKafka.span_id(),
                    offsetDateTime,
                    responseKafka.log_message(),
                    responseKafka.duration_ms(),
                    responseKafka.data().http_status()
            );
            responseLogRepository.save(responseLog);
        } catch (JsonProcessingException e) {
            log.error("Error parsing the message: {}", message);
            throw new RuntimeException(e);
        }
    }

    @SneakyThrows
    public void parsingMessageError(String message) {
        try {
            ObjectMapper objectMapper = new ObjectMapper();
            ErrorKafka errorKafka = objectMapper.readValue(message, ErrorKafka.class);
            OffsetDateTime offsetDateTime = OffsetDateTime.parse(errorKafka.timestamp());
            ErrorLog errorLog = new ErrorLog(
                    UUID.randomUUID(),
                    errorKafka.service_name(),
                    errorKafka.event_type(),
                    errorKafka.trace_id(),
                    errorKafka.span_id(),
                    offsetDateTime,
                    errorKafka.log_message()
            );
            errorLogRepository.save(errorLog);
        } catch (JsonProcessingException e) {
            log.error("Error parsing the message: {}", message);
            throw new RuntimeException(e);
        }
    }

    @SneakyThrows
    public List<LogProjection> getAllLogs(Authentication auth, String token) {
        UUID userId = jwtTokenProvider.getUserIdFromAuthentication(auth);

        if (tokenRepository.findById(token).isPresent()) {
            throw new UnauthorizedException("The user is not authorized");
        }

        List<LogProjection> logs = new ArrayList<>();

        requestLogRepository.findLogs().forEach(log ->
                logs.add(new LogProjection(
                        log.getServiceName(),
                        log.getEventType(),
                        log.getLogMessage(),
                        log.getTimestamp()
                ))
        );

        responseLogRepository.findLogs().forEach(log ->
                logs.add(new LogProjection(
                        log.getServiceName(),
                        log.getEventType(),
                        log.getLogMessage(),
                        log.getTimestamp()
                ))
        );

        return logs;
    }
    @SneakyThrows
    public List<LogProjection> getAllErrors(Authentication auth, String token) {
        UUID userId = jwtTokenProvider.getUserIdFromAuthentication(auth);

        if (tokenRepository.findById(token).isPresent()) {
            throw new UnauthorizedException("The user is not authorized");
        }

        List<LogProjection> errors = new ArrayList<>();

        errorLogRepository.findErrors().forEach(error ->
                errors.add(new LogProjection(
                        error.getServiceName(),
                        error.getEventType(),
                        error.getLogMessage(),
                        error.getTimestamp()
                ))
        );

        return errors;
    }

    @SneakyThrows
    public TraceDto getTraceByTraceId(Authentication auth, String token, String traceId) {
        UUID userId = jwtTokenProvider.getUserIdFromAuthentication(auth);

        if (tokenRepository.findById(token).isPresent()) {
            throw new UnauthorizedException("The user is not authorized");
        }

        TraceDto trace = new TraceDto();
        trace.setTraceId(traceId);

        // Собираем все логи по trace_id
        List<RequestLog> requestLogs = requestLogRepository.findByTraceId(traceId);
        List<ResponseLog> responseLogs = responseLogRepository.findByTraceId(traceId);
        List<ErrorLog> errorLogs = errorLogRepository.findByTraceId(traceId);

        // Создаем мапу для быстрого доступа к span по id
        Map<String, SpanDto> spanMap = new HashMap<>();

        // Обрабатываем RequestLogs
        requestLogs.forEach(log -> {
            SpanDto span = new SpanDto();
            span.setSpanId(log.getSpanId());
            span.setParentSpanId(log.getParentSpanId());
            span.setServiceName(log.getServiceName());

            if ("http_request_out".equals(log.getEventType().toString())) {
                span.setOperationName("OUT: " + log.getHttpMethod() + " " + log.getUrl());
                span.setStartTime(log.getTimestamp());
                span.getTags().put("http.method", log.getHttpMethod());
                span.getTags().put("url", log.getUrl());
            } else if ("http_request_in".equals(log.getEventType().toString())) {
                span.setOperationName("IN: " + log.getHttpMethod() + " " + log.getUrl());
                span.setStartTime(log.getTimestamp());
                span.getTags().put("http.method", log.getHttpMethod());
                span.getTags().put("url", log.getUrl());
            }

            spanMap.put(span.getSpanId(), span);
        });

        // Обрабатываем ResponseLogs
        responseLogs.forEach(log -> {
            SpanDto span = spanMap.get(log.getSpanId());
            if (span != null) {
                span.setEndTime(log.getTimestamp());
                span.setDurationMs(log.getDurationMs());
                if (log.getHttpStatus() != null) {
                    span.getTags().put("http.status", log.getHttpStatus());
                }
            }
        });

        // Обрабатываем ErrorLogs
        errorLogs.forEach(log -> {
            SpanDto span = spanMap.get(log.getSpanId());
            if (span != null) {
                span.getTags().put("error", "true");
                span.getTags().put("error.message", log.getLogMessage());
            }
        });

        trace.setSpans(new ArrayList<>(spanMap.values()));
        return trace;
    }

    @SneakyThrows
    public List<TraceInfoDto> getAllTraces(Authentication auth, String token) {
        UUID userId = jwtTokenProvider.getUserIdFromAuthentication(auth);

        if (tokenRepository.findById(token).isPresent()) {
            throw new UnauthorizedException("The user is not authorized");
        }

        List<String> traceIds = requestLogRepository.findAllTraceIds();

        return traceIds.stream()
                .map(traceId -> {
                    OffsetDateTime firstEventTime = requestLogRepository.findFirstEventTime(traceId)
                            .orElse(OffsetDateTime.now());

                    OffsetDateTime lastEventTime = requestLogRepository.findLastEventTime(traceId)
                            .orElse(OffsetDateTime.now());

                    Long spanCount = requestLogRepository.countSpansByTraceId(traceId);

                    boolean hasErrors = errorLogRepository.existsByTraceIdCustom(traceId);

                    return new TraceInfoDto(
                            traceId,
                            firstEventTime,
                            lastEventTime,
                            spanCount.intValue(),
                            hasErrors
                    );
                })
                .sorted(Comparator.comparing(TraceInfoDto::lastEventTime).reversed())
                .collect(Collectors.toList());
    }
}
