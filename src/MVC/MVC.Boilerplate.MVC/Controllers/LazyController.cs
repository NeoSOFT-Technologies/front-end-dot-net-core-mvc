using Microsoft.AspNetCore.Mvc;
using MVC.Boilerplate.Models.Lazy;
using MVC.Boilerplate.Service;

namespace MVC.Boilerplate.Controllers
{
    public class LazyController : Controller
    {
        int RecordsPerPage = 20;
        List<Person> PersonList;

        [HttpGet]
        public async Task<IActionResult> LoadList([FromQuery(Name = "pageNum")] int pageNum = 0)
        {
            //Checks for Ajax Request
            if(Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                var person = await GetPageData(pageNum);
                return PartialView("_PersonData", person);
            }
            else
            {

                ViewBag.RecordsPerPage = RecordsPerPage;
                ViewBag.Persons = await GetPageData(pageNum);
                ViewBag.TotalPersonCount = PersonList.Count;
                ViewBag.MaxPageCount = (PersonList.Count / RecordsPerPage);
                return View("Index");
            }
        }

        async Task<List<Person>> GetPageData(int pageNum)
        {
            PersonList = await LazyService.PersonList();
            //It defines from where in PersonList records should be fetched
            int from = pageNum * RecordsPerPage;

            var selectedData = PersonList.Skip(from-1).Take(RecordsPerPage).ToList();
            return selectedData;
        }
    }
}
