package hits.internship.NotificationService.service;

import hits.internship.NotificationService.config.KafkaProducer;
import hits.internship.NotificationService.entity.Mail;
import hits.internship.NotificationService.model.enumeration.DeadlineType;
import hits.internship.NotificationService.model.enumeration.KafkaMessageStatus;
import hits.internship.NotificationService.model.kafka.*;
import jakarta.mail.internet.MimeMessage;
import lombok.RequiredArgsConstructor;
import lombok.SneakyThrows;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.core.io.ClassPathResource;
import org.springframework.mail.javamail.JavaMailSender;
import org.springframework.mail.javamail.MimeMessageHelper;
import org.springframework.messaging.MessagingException;
import org.springframework.scheduling.annotation.Async;
import org.springframework.stereotype.Service;
import org.thymeleaf.TemplateEngine;
import org.thymeleaf.context.Context;

import java.time.LocalDate;
import java.time.temporal.ChronoUnit;
import java.util.Objects;


@Service
@Slf4j
@RequiredArgsConstructor
public class EmailService {

    private final JavaMailSender mailSender;
    private final TemplateEngine templateEngine;
    private final KafkaProducer kafkaProducer;

    @Value("${notification.topic.response}")
    private String responseTopic;

    @Async
    public void sendEmailWithThymeLeaf(Mail mail) {
        try {
            MimeMessage message = mailSender.createMimeMessage();
            MimeMessageHelper helper = new MimeMessageHelper(message, true);

            helper.setSubject(mail.getSubject());
            helper.setFrom("testmail6743@gmail.com");
            helper.setText(mail.getBody(), true);
            helper.setTo(mail.getTo());
            ClassPathResource classPathResource = new ClassPathResource("extension.jpg");
            helper.addAttachment(Objects.requireNonNull(classPathResource.getFilename()), classPathResource);
            mailSender.send(message);

            KafkaMessageResponse successfulMessage = new KafkaMessageResponse(mail.getId(), KafkaMessageStatus.completed, null);
            kafkaProducer.sendMessage(responseTopic, successfulMessage.toString());
        } catch (Exception ex) {
            log.error("Error when sending an email with id: {}", mail.getId());
            KafkaMessageResponse errorMessage = new KafkaMessageResponse(mail.getId(), KafkaMessageStatus.error, "Error when sending an email");
            kafkaProducer.sendMessage(responseTopic, errorMessage.toString());
        }
    }

    public void createRegistrationMail(Registration registration) {
        Context context = new Context();
        context.setVariable("email", registration.getEmail());
        context.setVariable("password", registration.getPassword());

        String process = templateEngine.process("Registration", context);
        Mail mail = new Mail(registration.getId(), registration.getEmail(), "Регистрация", process);
        sendEmailWithThymeLeaf(mail);
    }

    public void createChangePractise(ChangingPractise changingPractise) {
        Context context = new Context();
        context.setVariable("oldCompany", changingPractise.getOldCompanyName());
        context.setVariable("oldPosition", changingPractise.getOldPosition());
        context.setVariable("newCompany", changingPractise.getNewCompanyName());
        context.setVariable("newPosition", changingPractise.getNewPosition());

        switch (changingPractise.getNewStatus()) {
            case completed ->  context.setVariable("status", "Выполнено успешно");
            case denied ->  context.setVariable("status", "Отказано в переводе");
            case in_progress -> context.setVariable("status", "В процессе перевода");
        }

        String process = templateEngine.process("Changing-practise", context);
        Mail mail = new Mail(changingPractise.getId(), changingPractise.getEmail(), "Смена практики", process);
        sendEmailWithThymeLeaf(mail);
    }

    public void createDeadlineMail(Deadline deadline) {
        String url = deadline.getEvent().equals(DeadlineType.practise_diary) ? "https://ваш-сайт.ru/practise-diary" : "https://ваш-сайт.ru/internship-application";
        Context context = new Context();
        context.setVariable("deadline", deadline);
        context.setVariable("daysRemaining", calculateDaysRemaining(deadline.getDeadlineDate()));
        context.setVariable("url", url);
        String process = templateEngine.process("deadline-notification", context);
        Mail mail = new Mail(deadline.getId(), deadline.getEmail(), "Напоминание о дедлайне", process);
        sendEmailWithThymeLeaf(mail);
    }

    private long calculateDaysRemaining(LocalDate deadlineDate) {
        if (deadlineDate == null) {
            return 0;
        }
        return ChronoUnit.DAYS.between(LocalDate.now(), deadlineDate);
    }

    public void createAdmissionInternship(AdmissionInternship admissionInternship) {
        Context context = new Context();
        context.setVariable("companyName", admissionInternship.getCompanyName());
        context.setVariable("position", admissionInternship.getPosition());

        String process = templateEngine.process("Admission-internship", context);
        Mail mail = new Mail(admissionInternship.getId(), admissionInternship.getEmail(), "Зачисление на практику", process);
        sendEmailWithThymeLeaf(mail);
    }
}
