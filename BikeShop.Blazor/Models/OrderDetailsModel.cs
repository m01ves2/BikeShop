namespace BikeShop.Blazor.Models
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
        public List<OrderItemModel> Items { get; set; }
    }
}
