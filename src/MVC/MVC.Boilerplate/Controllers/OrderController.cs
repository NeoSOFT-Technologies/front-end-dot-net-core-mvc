using Microsoft.AspNetCore.Mvc;
using MVC.Boilerplate.Extensions;
using MVC.Boilerplate.Interfaces;
using MVC.Boilerplate.Models.DataTableProcessing;
using MVC.Boilerplate.Models.Order;


using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MVC.Boilerplate.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> GetOrders(Orders orders)
        {
            var orderDate = orders.OrderPlaced;
            string orderDateString = orderDate.ToString();

            HttpContext.Session.SetString("_orderDate", orderDateString);

            return View();
        }



        [HttpGet]
        public async Task<IActionResult> LoadTable(DataTablesResult tableParams)
        {
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

            int page = (tableParams.Start/tableParams.Length) + 1;
            int pageSize = tableParams.Length;
            var orderPlacedDate = HttpContext.Session.GetString("_orderDate");

            var result = await _orderService.GetOrderList(orderPlacedDate, page, pageSize);
            var orderList = result.Data;

            orderList = orderAscendingDirection ? orderList.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc).ToList() : orderList.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc).ToList();
            //var filteredResultsCount = orderList.Count();
            var filteredResultsCount = result.TotalCount;
            var totalResultsCount = result.TotalCount;

            return Json(new
            {
                draw = tableParams.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = orderList
                    .Skip(0)
                    .Take(tableParams.Length)
                    .ToList()
            });
        }
    }
}
