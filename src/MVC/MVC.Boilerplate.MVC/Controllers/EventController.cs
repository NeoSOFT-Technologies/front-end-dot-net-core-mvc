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
        private readonly ICategoryService _categoryService;
        private readonly INotyfService _notyf;
        private readonly IEventService _eventService;
        public EventController(IEventService eventService, INotyfService notyf, ICategoryService categoryService)
        {
            _eventService = eventService;
            _categoryService = categoryService;
            _notyf = notyf;
           
        }
        public async Task<IActionResult> GetEvents()
        {
            var result = await _eventService.GetEventList();
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> CreateEvent()
        {
            var categories = await _categoryService.GetAllCategories();
            ViewBag.categoryId = categories;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEvent events)
        {
            if (ModelState.IsValid)
            {
                var result = await _eventService.CreateEvent(events);
                var eventResult = await _eventService.GetEventList();
                _notyf.Success("Event created successfully");
                return View("GetEvents", eventResult);
            }
            return View();
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
            var categories = await _categoryService.GetAllCategories();
            ViewBag.categoryId = categories;
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEvent(GetByIdEvent updateEvent)
        {
            if(ModelState.IsValid)
            {
                var result = await _eventService.UpdateEvent(updateEvent);
                _notyf.Success("Event updated successfully");
                return RedirectToAction("UpdateEvent");
            }
            else
            {
                return View();
            }
            
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
