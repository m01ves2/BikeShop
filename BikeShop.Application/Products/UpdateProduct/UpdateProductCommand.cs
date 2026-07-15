using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;
using BikeShop.Domain.Entities;

namespace BikeShop.Application.Products.UpdateProduct
{
    public sealed record UpdateProductCommand(int Id, string Name, string Description, decimal Price, int StockQuantity, int CategoryId) : ICommand<Result>;
}
