using System.ComponentModel.DataAnnotations;

namespace BikeShop.Blazor.Models.Orders
{
    public sealed class CreateOrderModel
    {
        [Required]
        public string DeliveryAddress { get; set; } = string.Empty;

        [Required]
        public DateTime DeliveryAt { get; set; } = DateTime.Now.AddHours(1);

        [Required]
        public string CustomerPhone { get; set; } = string.Empty;
    }
}
