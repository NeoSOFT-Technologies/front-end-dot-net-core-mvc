using Microsoft.AspNetCore.Mvc;
using MVC.Boilerplate.Application.Exceptions;
using MVC.Boilerplate.Models;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using MVC.Boilerplate.Models.FileUpload;

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

        [HttpPost]
        public ActionResult Index(FileUploadModel fileUploadModel)
        {
            string uniquefileName = UploadFile(fileUploadModel);
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

        private string UploadFile(FileUploadModel fileUploadModel)
        {
            string uniqueFileName = null;
            if (fileUploadModel.File != null)
            {
                string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "FileFolder");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + fileUploadModel.File.FileName;
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    fileUploadModel.File.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
       


    }
}