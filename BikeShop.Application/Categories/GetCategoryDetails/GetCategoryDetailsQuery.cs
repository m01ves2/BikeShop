using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Categories.DTOs;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Categories.GetCategoryDetails
{
    public sealed record GetCategoryDetailsQuery(int Id) : IQuery<Result<CategoryDetailsDto>>;
}
