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
            try
            {
                await AccountService.Login(login);
            }
            catch
            {

            }


            return RedirectToAction("Index","Home");
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}
