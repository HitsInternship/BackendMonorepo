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

    @Value("${url.selection}")
    private String baseUrlSelection;

    @Value("${url.application}")
    private String baseUrlApplication;

    @Value("${url.profile}")
    private String baseUrlProfile;

    @Value("${url.practice-diary}")
    private String baseUrlPracticeDiary;

    @Value("${url.practice}")
    private String baseUrlPractice;

    @Value("${url.meeting}")
    private String baseUrlMeeting;

    @Async
    public void sendEmailWithThymeLeaf(Mail mail) {
        try {
            MimeMessage message = mailSender.createMimeMessage();
            MimeMessageHelper helper = new MimeMessageHelper(message, true);

            helper.setSubject(mail.getSubject());
            helper.setFrom("testmail6743@gmail.com");
            helper.setText(mail.getBody(), true);
            helper.setTo(mail.getTo());
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
        String url = deadline.getEvent().equals(DeadlineType.practise_diary) ? "http://185.239.51.32/practices" : "http://185.239.51.32/vacancies";
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

    public void createNewComment(NewComment newComment) {
        Context context = new Context();
        context.setVariable("fullName", newComment.getFullName());
        context.setVariable("message", newComment.getMessage());

        String theme = null;
        String url = null;

        switch (newComment.getCommentType()) {
            case practice_diary -> {
                context.setVariable("typeName", "дневнику практики");
                theme = "дневнику практики";
                url = baseUrlPracticeDiary + "/" + newComment.getTypeId();
            }
            case application -> {
                context.setVariable("typeName", "заявке на практику");
                theme = "заявке на практику";
                url = baseUrlApplication + "/" + newComment.getTypeId();
            }
            case characteristic -> {
                context.setVariable("typeName", "характеристике");
                theme = "характеристике";
                url = baseUrlProfile;
            }
            case selection -> {
                context.setVariable("typeName", "отбору на практику");
                theme = "отбору на практику";
                url = baseUrlSelection + "/" + newComment.getTypeId();
            }
            case vacancy_response -> {
                context.setVariable("typeName", "отклику на вакансию");
                theme = "отклику на вакансию";
                url = baseUrlSelection + "/" + newComment.getTypeId();
            }
        }

        context.setVariable("url", url);

        String process = templateEngine.process("New-comment", context);
        Mail mail = new Mail(newComment.getId(), newComment.getEmail(), "Новый комментарий к " + theme, process);
        sendEmailWithThymeLeaf(mail);
    }

    public void createRatedForPractice(RatedForPractice ratedForPractice) {
        Context context = new Context();
        context.setVariable("rate", ratedForPractice.getRate());
        context.setVariable("url", baseUrlPractice + "/" + ratedForPractice.getPracticeId().toString());

        String process = templateEngine.process("Rated-for-practice", context);
        Mail mail = new Mail(ratedForPractice.getId(), ratedForPractice.getEmail(), "Оценка за практику", process);
        sendEmailWithThymeLeaf(mail);
    }

    public void createMeeting(Meeting meeting) {
        Context context = new Context();
        context.setVariable("companyName", meeting.getCompanyName());
        context.setVariable("dateTime", meeting.getMeetingDateTime());
        context.setVariable("url", baseUrlMeeting + "/" + meeting.getMeetingId().toString());

        String process = templateEngine.process("Meeting", context);
        Mail mail = new Mail(meeting.getId(), meeting.getEmail(), "Компания назначила встречу", process);
        sendEmailWithThymeLeaf(mail);
    }

    public void createChangingPassword(ChangingPassword changingPassword) {
        Context context = new Context();
        context.setVariable("code", changingPassword.getCode());

        String process = templateEngine.process("Changing-password", context);
        Mail mail = new Mail(changingPassword.getId(), changingPassword.getEmail(), "Смена пароля", process);
        sendEmailWithThymeLeaf(mail);
    }
}
