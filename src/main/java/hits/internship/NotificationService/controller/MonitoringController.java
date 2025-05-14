package hits.internship.NotificationService.controller;

import hits.internship.NotificationService.exception.UnauthorizedException;
import hits.internship.NotificationService.model.output.LogProjection;
import hits.internship.NotificationService.model.output.TraceDto;
import hits.internship.NotificationService.model.output.TraceInfoDto;
import hits.internship.NotificationService.service.MonitoringService;
import lombok.RequiredArgsConstructor;
import lombok.SneakyThrows;
import org.springframework.http.HttpHeaders;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/monitoring")
@RequiredArgsConstructor
public class MonitoringController {

    private final MonitoringService monitoringService;

    @GetMapping("/log")
    @SneakyThrows
    public ResponseEntity<List<LogProjection>> getAllLogs(@RequestHeader(value = HttpHeaders.AUTHORIZATION, required = false) String authorizationHeader ) {
        Authentication auth = SecurityContextHolder.getContext().getAuthentication();
        if (authorizationHeader != null && authorizationHeader.startsWith("Bearer ")) {
            String token = authorizationHeader.substring(7);
            return ResponseEntity.ok(monitoringService.getAllLogs(auth, token));
        }
        throw new UnauthorizedException("Invalid Authorization header");
    }

    @GetMapping("/error")
    @SneakyThrows
    public ResponseEntity<List<LogProjection>> getAllErrors(@RequestHeader(value = HttpHeaders.AUTHORIZATION, required = false) String authorizationHeader ) {
        Authentication auth = SecurityContextHolder.getContext().getAuthentication();
        if (authorizationHeader != null && authorizationHeader.startsWith("Bearer ")) {
            String token = authorizationHeader.substring(7);
            return ResponseEntity.ok(monitoringService.getAllErrors(auth, token));
        }
        throw new UnauthorizedException("Invalid Authorization header");
    }

    @GetMapping("/{traceId}")
    @SneakyThrows
    public ResponseEntity<TraceDto> getTrace(@RequestHeader(value = HttpHeaders.AUTHORIZATION, required = false) String authorizationHeader, @PathVariable String traceId) {
        Authentication auth = SecurityContextHolder.getContext().getAuthentication();
        if (authorizationHeader != null && authorizationHeader.startsWith("Bearer ")) {
            String token = authorizationHeader.substring(7);
            return ResponseEntity.ok(monitoringService.getTraceByTraceId(auth, token, traceId));
        }
        throw new UnauthorizedException("Invalid Authorization header");
    }

    @GetMapping("/traces")
    @SneakyThrows
    public ResponseEntity<List<TraceInfoDto>> getAllTraces(@RequestHeader(value = HttpHeaders.AUTHORIZATION, required = false) String authorizationHeader) {
        Authentication auth = SecurityContextHolder.getContext().getAuthentication();
        if (authorizationHeader != null && authorizationHeader.startsWith("Bearer ")) {
            String token = authorizationHeader.substring(7);
            return ResponseEntity.ok(monitoringService.getAllTraces(auth, token));
        }
        throw new UnauthorizedException("Invalid Authorization header");
    }
}
