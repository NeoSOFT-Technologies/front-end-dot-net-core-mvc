using Microsoft.AspNetCore.Mvc;
using MVC.Boilerplate.Models.Account;

namespace MVC.Boilerplate.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login loginObj)
        {
            ViewBag.Login = loginObj;
            return RedirectToAction("Index","Home");
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}
