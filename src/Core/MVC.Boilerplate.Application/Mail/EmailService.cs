using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MVC.Boilerplate.Application.Models.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Boilerplate.Application.Mail
{
    public class EmailService : IEmailService
    {
        public EmailSettings _emailSettings { get; }
        public ILogger<EmailService> _logger { get; } 

        public EmailService(IOptions<EmailSettings> mailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = mailSettings.Value;
            _logger = logger;
        }
        public async Task<bool> SendEmail(Email email)
        {
            MailMessage message = new MailMessage(_emailSettings.FromAddress, email.To);
            message.Subject = email.Subject;
            message.IsBodyHtml = true;
            message.Body = email.Body;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;

            smtp.UseDefaultCredentials = false;
            NetworkCredential cred = new NetworkCredential(_emailSettings.FromAddress, _emailSettings.Password);
            smtp.Credentials = cred;
            smtp.Port = 587;
            smtp.Send(message);

            return true;
        }
        //public async Task<bool> SendEmail(Email email)
        //{
        //    var client = new SendGridClient(_emailSettings.ApiKey);

        //    var subject = email.Subject;
        //    var to = new EmailAddress(email.To);
        //    var emailBody = email.Body;

        //    var from = new EmailAddress
        //    {
        //        Email = _emailSettings.FromAddress,
        //        Name = _emailSettings.FromName
        //    };

        //    var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
        //    var response = await client.SendEmailAsync(sendGridMessage);

        //    _logger.LogInformation("Email sent");

        //    if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
        //        return true;

        //    _logger.LogError("Email sending failed");

        //    return false;
        //}
    }
}
