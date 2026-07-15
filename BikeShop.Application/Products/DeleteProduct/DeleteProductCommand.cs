using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Products.DeleteProduct
{
    public sealed record DeleteProductCommand(int Id) : ICommand<Result>;
}
