using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Admin.Products.DeleteProduct
{
    public sealed record AdminDeleteProductCommand(int Id) : ICommand<Result>;
}
