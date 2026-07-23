using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Products.DTOs;

namespace BikeShop.Application.Products.GetProductsByCategoryId
{
        public sealed record GetProductsByCategoryIdQuery(int CategoryId) : IQuery<Result<IReadOnlyList<ProductListItemDto>>>;
}
