using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MVC.Boilerplate.Application.Mail;
using MVC.Boilerplate.Application.Models.Mail;
using MVC.Boilerplate.Models.Account;
using MVC.Boilerplate.Service;

namespace MVC.Boilerplate.Controllers
{
    public class AccountController : Controller
    {
        public IEmailService _emailService;
        public AccountController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(Login login)
        {
            ViewBag.Login = login;   
            await AccountService.Login(login);

            Email _email = new Email();
       
            _email.To = "alfaizkhan147@gmail.com";
            _email.Body = "You logged in Successfully";
            _email.Subject = "Testing SendGrid Email";

            var result = _emailService.SendEmail(_email);

            return RedirectToAction("Index","Home");
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}
