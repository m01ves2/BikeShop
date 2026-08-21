namespace BikeShop.Application.Orders.Requests
{
    public sealed record CreateOrderRequest (string DeliveryAddress, DateTime DeliveryAt, string CustomerPhone);
}
