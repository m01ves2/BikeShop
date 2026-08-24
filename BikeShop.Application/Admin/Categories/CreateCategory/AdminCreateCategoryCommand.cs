using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Admin.Categories.CreateCategory
{
    public sealed record AdminCreateCategoryCommand(string Name) : ICommand<Result>;
}
