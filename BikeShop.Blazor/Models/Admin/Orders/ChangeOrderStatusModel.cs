namespace BikeShop.Blazor.Models.Admin.Orders
{
    public class ChangeOrderStatusModel
    {
        public int OrderId { get; set; }
        public string OrderStatusDto { get; set; } = string.Empty;
    }
}
