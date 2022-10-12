using Microsoft.AspNetCore.Mvc;
using MVC.Boilerplate.Application.Exceptions;
using MVC.Boilerplate.Models;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace MVC.Boilerplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHost)
        {
            _logger = logger;
            webHostEnvironment = webHost;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("An example of logging");
            var session = HttpContext.Session.GetString("UserName");
            if (session == null)
            {
                return View();
            }
            else
            {
                ViewBag.UserName = HttpContext.Session.GetString("UserName");
                return View();
            }

            
        }

        
    }
}