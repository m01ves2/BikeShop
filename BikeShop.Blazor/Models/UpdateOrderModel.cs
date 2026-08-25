namespace BikeShop.Blazor.Models
{
    public class UpdateOrderModel
    {
        public int OrderId { get; set; }
        public string DeliveryAddress { get; set; } = string.Empty;
        public DateTime DeliveryAt { get; set; }
        public string CustomerPhone { get; set; } = string.Empty;
    }
}
