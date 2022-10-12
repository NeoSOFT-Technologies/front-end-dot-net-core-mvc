using Microsoft.AspNetCore.Mvc;
using MVC.Boilerplate.Models.DataTableProcessing;
using MVC.Boilerplate.Service;

namespace MVC.Boilerplate.Controllers
{
    public class OrderController : Controller
    {
        public async Task<IActionResult> GetOrders()
        {
            int page = 1;
            int pageSize = 10;
            var result = await OrderService.GetKeyList(page, pageSize);
            return View(result);
            // return View();
        }



        [HttpGet]
        public async Task<IActionResult> LoadTable(DataTablesResult tableParams)
        {
            var searchBy = tableParams.Search?.Value;

            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;
            //if (tableParams.Order != null)
            //{
            //    orderCriteria = tableParams.Columns[tableParams.Order[0].Column].Data;
            //    orderAscendingDirection = tableParams.Order[0].Dir.ToString().ToLower() == "asc";
            //}
            //else
            //{
            //    orderCriteria = "Id";
            //    orderAscendingDirection = true;
            //}

            //var result = await _context.Employees.ToListAsync();
            int page = 1;
            int pageSize = 10;
            var result = await OrderService.GetKeyList(page, pageSize);
            var orderList = result.Data;

            //if (!string.IsNullOrEmpty(searchBy))
            //{
            //    result = result.Where(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()) ||
            //                               r.FirstSurname != null && r.FirstSurname.ToUpper().Contains(searchBy.ToUpper()) ||
            //                               r.SecondSurname != null && r.SecondSurname.ToUpper().Contains(searchBy.ToUpper()) ||
            //                               r.Street != null && r.Street.ToUpper().Contains(searchBy.ToUpper()) ||
            //                               r.Phone != null && r.Phone.ToUpper().Contains(searchBy.ToUpper()) ||
            //                               r.ZipCode != null && r.ZipCode.ToUpper().Contains(searchBy.ToUpper()) ||
            //                               r.Country != null && r.Country.ToUpper().Contains(searchBy.ToUpper()) ||
            //                               r.Notes != null && r.Notes.ToUpper().Contains(searchBy.ToUpper()))
            //        .ToList();
            //}

            if(!string.IsNullOrEmpty(searchBy))
            {
                orderList = orderList.Where(r => r.Id != null && r.Id.ToString().Contains(searchBy) ||
                                                 r.OrderTotal != null && r.OrderTotal.ToString().Contains(searchBy) ||
                                                 r.OrderPlaced != null && r.OrderPlaced.ToString().Contains(searchBy)).ToList();

            }


            //orderList = orderAscendingDirection ? orderList.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc).ToList() : orderList.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc).ToList();
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
