using Microsoft.AspNetCore.Mvc;
using MVC.Boilerplate.Models.Event;
using MVC.Boilerplate.Service;

namespace MVC.Boilerplate.Controllers
{
    public class EventController : Controller
    {
        public async Task<IActionResult> GetEvents()
        {
            var result = await EventService.GetEventList();
            return View(result);
        }
        [HttpGet]
        public IActionResult CreateEvent()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventDetails events)
        {
            var result = await EventService.CreateEvents(events);
            return View();
        }
    }
}
