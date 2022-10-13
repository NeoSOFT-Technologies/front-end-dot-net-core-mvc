using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using MVC.Boilerplate.Interfaces;
using MVC.Boilerplate.Models.Event.Commands;
using MVC.Boilerplate.Models.Event.Queries;
using MVC.Boilerplate.Service;

namespace MVC.Boilerplate.Controllers
{
    public class EventController : Controller
    {
        private readonly IDataProtector _protector;
        private readonly IEventService _eventService;
        public EventController(IEventService eventService, IDataProtectionProvider provider)
        {
            _eventService = eventService;
            _protector = provider.CreateProtector("");
        }
        public async Task<IActionResult> GetEvents()
        {
            var result = await _eventService.GetEventList();
            return View(result);
        }
        [HttpGet]
        public IActionResult CreateEvent()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEvent events)
        {
            var result = await _eventService.CreateEvent(events);
            return View();
        }

        public async Task<IActionResult> GetEventById(string eventId)
        {
            var result = await _eventService.GetEventById(eventId);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateEvent([FromQuery(Name = "eventId")] string eventId)
        {
            var result = await _eventService.GetEventById(eventId);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEvent(GetByIdEvent updateEvent)
        {
            //string id = _protector.Unprotect(updateEvent.EventId);
            //updateEvent.EventId = id;
            var result = await _eventService.UpdateEvent(updateEvent);
            return View();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteEvent(string eventId)
        {
            await _eventService.DeleteEvent(eventId);
            var result = await _eventService.GetEventList();
            return View("GetEvents",result);
        }
    }
}
