using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using NewProject.Services.Entities.User;
using NewProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using NewProject.Domain.Entities.User;

namespace NewProject.Services.Services
{
    public class MailService 
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task<bool> SendEmail(UserRegisterTemp request)
        {
                MimeMessage emailMessage = new MimeMessage();

                MailboxAddress emailFrom = new MailboxAddress("Acemic", "rizvan.dcs@gmail.com");
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
                emailClient.Connect("smtp.gmail.com", 465, true);
                emailClient.Authenticate("rizvan.dcs@gmail.com", "mbmnouohsqikztst");
                emailClient.Send(emailMessage);
                emailClient.Disconnect(true);
                emailClient.Dispose();

            return true;
        }

    }
}
