using MVC.Boilerplate.Application.Models.Responses;
using MVC.Boilerplate.Models.Order;

namespace MVC.Boilerplate.Interfaces
{
    public interface IOrderService
    {
        Task<PagedResponse<IEnumerable<Orders>>> GetOrderList(int page, int pageSize);
    }
}
