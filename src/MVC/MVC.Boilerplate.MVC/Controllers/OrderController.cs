using Microsoft.AspNetCore.Mvc;
using MVC.Boilerplate.Service;

namespace MVC.Boilerplate.Controllers
{
    public class OrderController : Controller
    {
        public async Task<IActionResult> GetOrders()
        {
            var result = await OrderService.GetKeyList();
            return View(result);
        }
    }
}
