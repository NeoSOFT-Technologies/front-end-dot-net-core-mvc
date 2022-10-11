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

            if (ModelState.IsValid)
            {
                

                LoginResponse loginResponse = await AccountService.Login(login);

                

                if (loginResponse.UserName != null)
                {
                    HttpContext.Session.SetString("UserName", loginResponse.UserName);
                    _notyf.Success("Logged In Successfully");
                }
                else
                {
                    ViewBag.Error = loginResponse.Message;
                    _notyf.Error(loginResponse.Message);
                    return View();

                }

                // Email Configuration
                Email _email = new Email();
                _email.To = loginResponse.Email;
                _email.Body = "You logged in Successfully";
                _email.Subject = "Confirmation Email";
                var result = _emailService.SendEmail(_email);


                return RedirectToAction("Index", "Home");

            }
            else
            {
                return View();
            }
            


           
        }


        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
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
                if (register.Message == null)
                {
                    // Email
                    Email _email = new Email();
                    _email.To = register.Email;
                    _email.Body = "You Registered Successfully";
                    _email.Subject = "Confirmation Email";
                    var result = _emailService.SendEmail(_email);
                    _notyf.Error("Registered Successfully");

                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    _notyf.Success("Something");
                    return View();

                }
               
            }
            return View();

        }
    }
    }

