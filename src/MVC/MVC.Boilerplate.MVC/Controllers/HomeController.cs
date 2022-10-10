using Microsoft.AspNetCore.Mvc;
using MVC.Boilerplate.Application.Helper.ApiHelper;
using MVC.Boilerplate.Models;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace MVC.Boilerplate.Controllers
{
    [ApiController]
    [Route("Home")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApiClient<Object> _client;

        public HomeController(ILogger<HomeController> logger, IApiClient<Object> client)
        {
            _logger = logger;
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _client.GetAllAsync("api/users");
            return View();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _client.GetByIdAsync($"api/users/{id}");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Object user)
        {
            var response = await _client.PostAsync("api/users",user);
            return View();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Object user)
        {
            var response = await _client.PostAsync("api/users/2", user);
            return View();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"api/users/{id}");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}