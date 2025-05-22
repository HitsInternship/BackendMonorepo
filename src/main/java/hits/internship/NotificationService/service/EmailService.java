package hits.internship.NotificationService.service;

import hits.internship.NotificationService.entity.Mail;
import hits.internship.NotificationService.model.enumeration.DeadlineType;
import hits.internship.NotificationService.model.kafka.AdmissionInternship;
import hits.internship.NotificationService.model.kafka.ChangingPractise;
import hits.internship.NotificationService.model.kafka.Deadline;
import hits.internship.NotificationService.model.kafka.Registration;
import jakarta.mail.internet.InternetAddress;
import jakarta.mail.internet.MimeMessage;
import lombok.RequiredArgsConstructor;
import lombok.SneakyThrows;
import org.springframework.core.io.ClassPathResource;
import org.springframework.mail.SimpleMailMessage;
import org.springframework.mail.javamail.JavaMailSender;
import org.springframework.mail.javamail.MimeMessageHelper;
import org.springframework.messaging.MessagingException;
import org.springframework.scheduling.annotation.Async;
import org.springframework.stereotype.Service;
import org.thymeleaf.TemplateEngine;
import org.thymeleaf.context.Context;

import java.time.LocalDate;
import java.time.OffsetDateTime;
import java.time.temporal.ChronoUnit;
import java.util.Objects;

import static org.unbescape.html.HtmlEscape.escapeHtml;

@Service
@RequiredArgsConstructor
public class EmailService {

    private final JavaMailSender mailSender;
    private final TemplateEngine templateEngine;





















    @Async
    public void sendSimpleEmail(Mail mail) {
        SimpleMailMessage message = new SimpleMailMessage();
        message.setTo(mail.getTo());
        message.setSubject(mail.getSubject());
        message.setText(mail.getBody());

        mailSender.send(message);
    }

    @Async
    @SneakyThrows
    public void sendHTMLEmail(Mail mail){
        MimeMessage message = mailSender.createMimeMessage();

        message.setFrom(new InternetAddress("HITS@gmail.com"));
        for (String recipient: mail.getTo()){
            message.addRecipients(MimeMessage.RecipientType.TO, recipient);
        }
        message.setSubject(mail.getSubject());
        message.setContent(mail.getBody(), "text/html; charset=utf-8");

        mailSender.send(message);
    }

    @Async
    @SneakyThrows
    public void sendEmailWithThymeLeaf(Mail mail) throws MessagingException {

        MimeMessage message = mailSender.createMimeMessage();
        MimeMessageHelper helper = new MimeMessageHelper(message, true);

        helper.setSubject(mail.getSubject());
        helper.setFrom("testmail6743@gmail.com");
        helper.setText(mail.getBody(), true);
        helper.setTo(mail.getTo());
        ClassPathResource classPathResource = new ClassPathResource("extension.jpg");
        helper.addAttachment(Objects.requireNonNull(classPathResource.getFilename()), classPathResource);
        mailSender.send(message);
    }

    @Async
    @SneakyThrows
    public void sendEmailWithAttachment(Mail mail) throws MessagingException {
        for (String recipient: mail.getTo()){
            MimeMessage message = mailSender.createMimeMessage();
            MimeMessageHelper helper = new MimeMessageHelper(message, true);

            helper.setFrom("testmail6743@gmail.com");
            helper.setTo(recipient);
            helper.setSubject("Testing Mail API With Attachment");
            helper.setText("Please find the attached document below");

            ClassPathResource classPathResource = new ClassPathResource("hello.jpg");
            helper.addAttachment(Objects.requireNonNull(classPathResource.getFilename()), classPathResource);

            mailSender.send(message);
        }
    }

    public void createRegistrationMail(Registration registration) {
        Context context = new Context();
        context.setVariable("email", registration.getEmail());
        context.setVariable("password", registration.getPassword());

        String process = templateEngine.process("Registration", context);
        Mail mail = new Mail(new String[]{registration.getEmail()}, "Регистрация", process);
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

        String process = templateEngine.process("ChangingPractise", context);
        Mail mail = new Mail(new String[]{changingPractise.getEmail()}, "Смена практики", process);
        sendEmailWithThymeLeaf(mail);
    }

    public void createDeadlineMail(Deadline deadline) {
        String url = deadline.getEvent().equals(DeadlineType.practise_diary) ? "https://ваш-сайт.ru/practise-diary" : "https://ваш-сайт.ru/internship-application";
        Context context = new Context();
        context.setVariable("deadline", deadline);
        context.setVariable("daysRemaining", calculateDaysRemaining(deadline.getDeadlineDate()));
        context.setVariable("url", url);
        String process = templateEngine.process("deadline-notification", context);
        Mail mail = new Mail(new String[]{deadline.getEmail()}, "Напоминание о дедлайне", process);
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
        Mail mail = new Mail(new String[]{admissionInternship.getEmail()}, "Зачисление на практику", process);
        sendEmailWithThymeLeaf(mail);
    }
}
