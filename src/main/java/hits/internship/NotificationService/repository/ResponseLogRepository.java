package hits.internship.NotificationService.repository;

import hits.internship.NotificationService.entity.ResponseLog;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;

import java.util.List;
import java.util.UUID;

public interface ResponseLogRepository extends JpaRepository<ResponseLog, UUID> {
    @Query("SELECT r FROM ResponseLog r")
    List<ResponseLog> findLogs();

    List<ResponseLog> findByTraceId(String traceId);
}
