using NewProject.Domain.Entities.User;
using MimeKit;
using MailKit.Net.Smtp;

namespace NewProject.Services
{
    public class MailService
    {
        public void SendEmail(UserRegisterTemp request, MailSettings Mailsettingdata)
        {
            MimeMessage emailMessage = new MimeMessage();

            //MailboxAddress emailFrom = new MailboxAddress("Acemic", "rizvan.dcs@gmail.com");
            MailboxAddress emailFrom = new MailboxAddress(Mailsettingdata.DisplayName, Mailsettingdata.EmailFrom);
            emailMessage.From.Add(emailFrom);

            MailboxAddress emailTo = new MailboxAddress(request.FirstName, request.EmailAddress);
            emailMessage.To.Add(emailTo);

            emailMessage.Subject = "Welcome To Acemic";

            string FilePath = Directory.GetCurrentDirectory() + "\\MailHtml\\WelcomeEmail.html";
            string EmailTemplateText = System.IO.File.ReadAllText(FilePath);

            EmailTemplateText = string.Format(EmailTemplateText, request.FirstName, request.Otp, DateTime.Now.Date.ToShortDateString());
            EmailTemplateText = EmailTemplateText.Replace("{0}", request.FirstName);
            EmailTemplateText = EmailTemplateText.Replace("{1}", request.Otp.ToString());

            BodyBuilder emailBodyBuilder = new BodyBuilder();
            emailBodyBuilder.HtmlBody = EmailTemplateText;
            emailMessage.Body = emailBodyBuilder.ToMessageBody();

            SmtpClient emailClient = new SmtpClient();
            //emailClient.Connect(_emailSettings.Host, _emailSettings.Port, _emailSettings.UseSSL);
            //emailClient.Authenticate(_emailSettings.EmailId, _emailSettings.Password);
            //emailClient.Connect("smtp.gmail.com", 465, true);
            //emailClient.Authenticate("rizvan.dcs@gmail.com", "mbmnouohsqikztst");
            emailClient.Connect(Mailsettingdata.Host, Mailsettingdata.Port, true);
            emailClient.Authenticate(Mailsettingdata.Mail, Mailsettingdata.Password);
            emailClient.Send(emailMessage);
            emailClient.Disconnect(true);
            emailClient.Dispose();
        }
    }
}
