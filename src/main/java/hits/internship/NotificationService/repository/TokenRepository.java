package hits.internship.NotificationService.repository;

import hits.internship.NotificationService.entity.Token;
import org.springframework.data.jpa.repository.JpaRepository;

public interface TokenRepository extends JpaRepository<Token, String> {
}
