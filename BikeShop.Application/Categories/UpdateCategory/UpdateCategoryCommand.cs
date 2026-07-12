using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Categories.UpdateCategory
{
    public sealed record UpdateCategoryCommand(int Id, string Name) : ICommand<Result>;
}
