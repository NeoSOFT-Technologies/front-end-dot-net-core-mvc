using Microsoft.AspNetCore.Mvc;
using MVC.Boilerplate.Models.FileUpload;
using System.IO;
using System.Data;
using System.Windows;

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

            if (fileUploadModel.File != null)

                ViewData["File"] = fileUploadModel.File.FileName;

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
                    fileUploadModel.FileUrl = fileStream.Name;
                    fileUploadModel.File.CopyTo(fileStream);
                }
                    ViewData["FileUploadPath"] = filePath;
                ViewData["FileUploadName"] = uniqueFileName;
                
            }
            return uniqueFileName;
        }

        public IActionResult Delete(string FileName)
        {
            if (FileName != null)
            {
                string ExitingFile = Path.Combine(webHostEnvironment.WebRootPath, "FileFolder/") + FileName;
           
                System.IO.File.Delete(ExitingFile);
            }
            return RedirectToAction("Index", "FileUpload");
        }

       public FileResult DownloadFile(string FileName)
        {
            //Build the File Path.
            string path = Path.Combine(this.webHostEnvironment.WebRootPath, "FileFolder/") + FileName;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", FileName);
        }



    }
}
