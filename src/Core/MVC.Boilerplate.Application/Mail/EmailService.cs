using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MVC.Boilerplate.Application.Models.Mail;
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
    }
}
