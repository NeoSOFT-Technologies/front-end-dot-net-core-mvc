using Microsoft.AspNetCore.Mvc;
using MVC.Boilerplate.Models.FileUpload;
using System.IO;
using System.Data;

namespace MVC.Boilerplate.Controllers
{
    public class FileUploadController : Controller
    {

        private readonly ILogger<FileUploadController> _logger;
        private readonly IWebHostEnvironment webHostEnvironment;

        public FileUploadController(ILogger<FileUploadController> logger, IWebHostEnvironment webHost)
        {
            _logger = logger;
            webHostEnvironment = webHost;
        }
        public IActionResult Index(FileUploadModel fileUploadModel)
        {
            UploadFile(fileUploadModel);
            return View();
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
                ViewData["FileUploadPath"] = filePath;
                ViewData["FileUploadName"] = uniqueFileName;
                
            }
            return uniqueFileName;
        }


       

    }
}
