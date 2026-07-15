using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Products.GetProductDetails
{
    public sealed record GetProductDetailsQuery(int Id) : IQuery<Result<ProductDetailsDto>>;
}
