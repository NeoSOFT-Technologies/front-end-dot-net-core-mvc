using MVC.Boilerplate.Models.Order;
using MVC.Boilerplate.Application.Helper.ApiHelper;
using MVC.Boilerplate.Interfaces;
using MVC.Boilerplate.Application.Models.Responses;

namespace MVC.Boilerplate.Service
{
    public class OrderService: IOrderService
    {
        private readonly IApiClient<Orders> _client;
        public readonly ILogger<CategoryService> _logger;

        public OrderService(IApiClient<Orders> client, ILogger<CategoryService> logger)
        {
            _client = client;
            _logger = logger;
        }
        public async Task<PagedResponse<IEnumerable<Orders>>> GetOrderList(int page, int pageSize)
        {
            _logger.LogInformation("GetOrderList Service initiated.");
            var orders = await _client.GetPagedAsync("Order?date=2022-02-21&page=" + page + "&size=" + pageSize);
            _logger.LogInformation("GetOrderList Service completed.");
            return orders;
        }
    }
}
