using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Products.GetProducts
{
    public sealed record GetProductsQuery : IQuery<Result<IReadOnlyList<ProductListItemDto>>>;
}
