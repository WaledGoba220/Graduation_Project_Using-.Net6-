using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace E_Exam.Utility.EmailSender
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmail(string email, string subject, string htmlMessage)
        {
            var fromMail = "Abdallafathy1121@outlook.com";
            var fromPassword = "Abdalla0159357";

            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("GOBA", fromMail));
            message.To.Add(new MailboxAddress("Confirmation Email", email));

            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"{htmlMessage}"
            };

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync("smtp-mail.outlook.com", 587, false);
                smtp.Authenticate(fromMail, fromPassword);
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);
            }
        }
    }
}
