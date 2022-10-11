using AspNetCoreHero.ToastNotification.Abstractions;
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
        private readonly INotyfService _notyf;

        public AccountController(IEmailService emailService,INotyfService notfy)
        {
            _notyf = notfy;
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
            if (login.Email == null && login.Password == null)
            {
                return View();
            }
            else
            {

                login = await AccountService.Login(login);


                if (login.UserName != null)
                {
                    HttpContext.Session.SetString("UserName", login.UserName);
                    _notyf.Success("Logged In Successfully");
                }
                else
                {
                    ViewBag.Error = login.Message;
                    _notyf.Error(login.Message);
                    return View();
                }


                return RedirectToAction("Index", "Home");
            }


            // Email Configuration
            Email _email = new Email();
            _email.To = "alfaizkhan147@gmail.com";
            _email.Body = "You logged in Successfully";
            _email.Subject = "Testing SendGrid Email";
            var result = _emailService.SendEmail(_email);
        }
            [HttpGet]
            public ActionResult Logout()
            {
                //HttpContext.Session.Clear();
                HttpContext.Session.Remove("UserName");
                _notyf.Success("Logged Out Successfully");
                return RedirectToAction("Login", "Account");
            }

            public IActionResult Register()
            {
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> Register(Register register)
            {


                if (ModelState.IsValid)
                { //checking model state
                    await AccountService.Register(register);
                    return RedirectToAction("Login", "Account");
                }
                return View();

            }
        }
    }

