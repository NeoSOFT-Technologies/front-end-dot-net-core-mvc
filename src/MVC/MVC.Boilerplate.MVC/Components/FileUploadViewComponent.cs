using Microsoft.AspNetCore.Mvc;

namespace MVC.Boilerplate.Components
{
    public class FileUploadViewComponent : ViewComponent
    {
        public FileUploadViewComponent()
        {

        }

        public IViewComponentResult Invoke(string FieldName)
        {
            
            return View("FileUpload", FieldName );
        }
    }
}
