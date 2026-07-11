using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Categories.CreateCategory
{
    public sealed record CreateCategoryCommand(string Name) : ICommand<Result>;
}
