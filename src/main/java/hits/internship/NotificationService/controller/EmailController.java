package hits.internship.NotificationService.controller;

import hits.internship.NotificationService.entity.Mail;
import hits.internship.NotificationService.service.EmailService;
import jakarta.mail.MessagingException;
import lombok.RequiredArgsConstructor;
import lombok.SneakyThrows;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.mail.MailException;
import org.springframework.web.bind.annotation.*;

import java.io.FileNotFoundException;

@RestController
@RequiredArgsConstructor
@RequestMapping("/email")
public class EmailController {

    private final EmailService emailService;

    @PostMapping("/simple")
    public void sendSimpleEmail(@RequestBody Mail mail){
        emailService.sendSimpleEmail(mail);
    }

    @PostMapping("/html")
    public void sendHTMLEmail(@RequestBody Mail mail) throws MessagingException {
        emailService.sendHTMLEmail(mail);
    }

    @PostMapping("/template")
    public void sendEmailWithThymeLeaf(@RequestBody Mail mail) throws MessagingException {
        emailService.sendEmailWithThymeLeaf(mail);
    }

    @PostMapping("/attachment")
    public void sendEmailWithAttachment(@RequestBody Mail mail) throws MessagingException {
        emailService.sendEmailWithAttachment(mail);
    }

}
