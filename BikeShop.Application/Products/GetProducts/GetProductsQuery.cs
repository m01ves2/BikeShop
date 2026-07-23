using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Products.DTOs;

namespace BikeShop.Application.Products.GetProducts
{
    public sealed record GetProductsQuery : IQuery<Result<IReadOnlyList<ProductListItemDto>>>;
}
