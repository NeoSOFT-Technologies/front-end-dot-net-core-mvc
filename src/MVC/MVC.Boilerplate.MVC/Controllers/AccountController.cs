using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MVC.Boilerplate.Models.Account;
using MVC.Boilerplate.Service;

namespace MVC.Boilerplate.Controllers
{
    public class AccountController : Controller
    {
        private readonly INotyfService _notyf;

        public AccountController(INotyfService notfy)
        {
            _notyf = notfy;
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
