namespace BikeShop.Application.Orders.Requests
{
    public sealed record UpdateOrderRequest(int OrderId, string DeliveryAddress, DateTime DeliveryAt, string CustomerPhone);
}
