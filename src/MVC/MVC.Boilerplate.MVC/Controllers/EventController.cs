using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using MVC.Boilerplate.Interfaces;
using MVC.Boilerplate.Models.Event.Commands;
using MVC.Boilerplate.Models.Event.Queries;
using MVC.Boilerplate.Service;

namespace MVC.Boilerplate.Controllers
{
    public class EventController : Controller
    {
        private readonly INotyfService _notyf;
        private readonly IEventService _eventService;
        public EventController(IEventService eventService, INotyfService notyf)
        {
            _eventService = eventService;
            _notyf = notyf;
           
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
            var eventResult = await _eventService.GetEventList();
            _notyf.Success("Event created successfully");
            return View("GetEvents", eventResult);
        }

        public async Task<IActionResult> GetEventById(string eventId)
        {
            var result = await _eventService.GetEventById(eventId);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateEvent(string id)
        {
            var result = await _eventService.GetEventById(id);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEvent(GetByIdEvent updateEvent)
        {
            var result = await _eventService.UpdateEvent(updateEvent);
            var updatedResult = await _eventService.GetEventList();
            return View("GetEvents", updatedResult);
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
