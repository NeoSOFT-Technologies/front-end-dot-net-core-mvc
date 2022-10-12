using Microsoft.AspNetCore.Mvc;
using MVC.Boilerplate.Interfaces;
using MVC.Boilerplate.Models.Category.Commands;

namespace MVC.Boilerplate.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var d = await _categoryService.GetAllCategories();
            var dwithevents = await _categoryService.GetAllCategoriesWithEvents(true);
            var create = await _categoryService.CreateCategory(new CreateCategory { Name = "Test 1" });
            return View();
        }
    }
}
