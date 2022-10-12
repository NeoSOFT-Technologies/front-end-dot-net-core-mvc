using Microsoft.AspNetCore.Mvc;
using MVC.Boilerplate.Application.Exceptions;
using MVC.Boilerplate.Models;
using System.Diagnostics;

namespace MVC.Boilerplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("An example of logging");
            //ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = "abc";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}