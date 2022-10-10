namespace MVC.Boilerplate.Models.Order
{
    public class Orders
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        //public GetAllOrderDetails Data { get; set; }

        public List<OrderDetails> Data { get; set; }

    }

    public class GetAllOrderDetails
    {
        public List<OrderDetails> OrderDetailsData { get; set; }
    }

    public class OrderDetails
    {
        public Guid Id { get; set; }
        public int OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
    }
}
