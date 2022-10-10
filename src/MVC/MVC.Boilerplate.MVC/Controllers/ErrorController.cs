using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Boilerplate.Controllers
{
    //[Route("Error")]
    public class ErrorController : Controller
    {
       [Route("ErrorHandler/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Response.StatusCode;
            switch (statusCode)
            {
                case 401:
                    return RedirectToAction("Error401");
                    break;
                case 404:
                    return RedirectToAction("Error404");
                    break;
                case 500:
                    return RedirectToAction("Error500");
                    break;
            }
            return View("Error500");
        }
        public IActionResult Error401()
        {
            return View("Error401");
        }
        public IActionResult Error404()
        {
            return View("Error404");
        }
        public IActionResult Error500()
        {
            return View("Error500");
        }
    }
}
