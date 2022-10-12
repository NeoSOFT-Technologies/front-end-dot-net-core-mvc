using Microsoft.AspNetCore.Mvc;
using MVC.Boilerplate.Interfaces;
using MVC.Boilerplate.Models.Lazy;
using MVC.Boilerplate.Service;

namespace MVC.Boilerplate.Controllers
{
    public class LazyController : Controller
    {
        private readonly ILogger<LazyController> _logger;
        private readonly ILazyService _lazyService;
        int RecordsPerPage = 20;
        List<Person> PersonList;
        public LazyController(ILogger<LazyController> logger,ILazyService lazyService)
        {
            _logger = logger;
            _lazyService = lazyService;
        }
        [HttpGet]
        public async Task<IActionResult> LoadList([FromQuery(Name = "pageNum")] int pageNum = 0)
        {
            _logger.LogInformation("LoadList Action initiated");
            //Checks for Ajax Request
            if(Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                var person = await GetPageData(pageNum);
                _logger.LogInformation("LoadList Action completed");
                return PartialView("_PersonData", person);
            }
            else
            {

                ViewBag.RecordsPerPage = RecordsPerPage;
                ViewBag.Persons = await GetPageData(pageNum);
                ViewBag.TotalPersonCount = PersonList.Count;
                ViewBag.MaxPageCount = (PersonList.Count / RecordsPerPage);

                _logger.LogInformation("LoadList Action completed");
                return View("Index");
            }
        }

        async Task<List<Person>> GetPageData(int pageNum)
        {
            PersonList = await _lazyService.PersonList();
            //It defines from where in PersonList records should be fetched
            int from = pageNum * RecordsPerPage;

            var selectedData = PersonList.Skip(from-1).Take(RecordsPerPage).ToList();
            return selectedData;
        }
    }
}
