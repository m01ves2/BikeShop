namespace BikeShop.Blazor.Models
{
    public class ChangeOrderStatusModel
    {
        public int OrderId { get; set; }
        public string OrderStatusDto { get; set; } = string.Empty;
    }
}
