using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MVC.Boilerplate.Models.Account;
using MVC.Boilerplate.Service;

namespace MVC.Boilerplate.Controllers
{
    public class AccountController : Controller
    {
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
                var result = await AccountService.Login(login);
                string Username = result.UserName;
                HttpContext.Session.SetString("UserName", Username);
                return RedirectToAction("Index", "Home");
            }
            
        }

        [HttpGet]
        public ActionResult Logout()
        {
            //HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
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
