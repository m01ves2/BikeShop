using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Categories.DTOs;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Categories.GetCategories
{
    public sealed record GetCategoriesQuery : IQuery<Result<IReadOnlyList<CategoryListItemDto>>>;
}
