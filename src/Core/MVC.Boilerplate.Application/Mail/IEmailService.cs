using MVC.Boilerplate.Application.Models.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Boilerplate.Application.Mail
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
