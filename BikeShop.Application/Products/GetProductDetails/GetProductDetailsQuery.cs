using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Products.DTOs;

namespace BikeShop.Application.Products.GetProductDetails
{
    public sealed record GetProductDetailsQuery(int Id) : IQuery<Result<ProductDetailsDto>>;
}
