using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Orders.DTOs;

namespace BikeShop.Application.Orders.CreateOrder
{
    public sealed record CreateOrderCommand(int ApplicationUserId, 
        string DeliveryAddress, 
        DateTime DeliveryAt, 
        string CustomerPhone) : ICommand<Result<CreateOrderResultDto>>;
}
