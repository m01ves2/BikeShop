using BikeShop.Blazor.Models.Orders;

namespace BikeShop.Blazor.Models.Admin.Orders
{
    public class OrderDetailsModel
    {
        public int Id { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime DeliveryAt { get; set; }
        public string DeliveryAddress { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string? CourierPhone { get; set; }
        public List<OrderItemModel> Items { get; set; } = [];
        public List<string> AllowedStatuses { get; set; } = [];
    }
}
