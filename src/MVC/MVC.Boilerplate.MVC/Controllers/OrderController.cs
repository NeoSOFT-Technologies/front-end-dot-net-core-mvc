using Microsoft.AspNetCore.Mvc;
using MVC.Boilerplate.Extensions;
using MVC.Boilerplate.Interfaces;
using MVC.Boilerplate.Models.DataTableProcessing;
using MVC.Boilerplate.Service;

namespace MVC.Boilerplate.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
    }
        public async Task<IActionResult> GetOrders()
        {
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> LoadTable(DataTablesResult tableParams)
        {
            var searchBy = tableParams.Search?.Value;

            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;

            if (tableParams.Order != null)
            {
                orderCriteria = tableParams.Columns[tableParams.Order[0].Column].Data;
                orderAscendingDirection = tableParams.Order[0].Dir.ToString().ToLower() == "asc";
            }
            else
            {
                orderCriteria = "Id";
                orderAscendingDirection = true;
            }

            int page = 1;
            int pageSize = 10;
            var result = await _orderService.GetOrderList(page, pageSize);
            var orderList = result.Data;

            if (!string.IsNullOrEmpty(searchBy))
            {
                orderList = orderList.Where(r => r.Id != null && r.Id.ToString().Contains(searchBy) ||
                                                 r.OrderTotal != null && r.OrderTotal.ToString().Contains(searchBy) ||
                                                 r.OrderPlaced != null && r.OrderPlaced.ToString().Contains(searchBy)).ToList();

            }


            orderList = orderAscendingDirection ? orderList.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc).ToList() : orderList.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc).ToList();
            var filteredResultsCount = orderList.Count();
            var totalResultsCount = result.TotalCount;
            return Json(new
            {
                draw = tableParams.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = orderList
                    .Skip(tableParams.Start)
                    .Take(tableParams.Length)
                    .ToList()
            });
        }
    }
}
