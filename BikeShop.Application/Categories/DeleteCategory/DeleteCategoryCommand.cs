using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Categories.DeleteCategory
{
    public sealed record DeleteCategoryCommand(int Id) : ICommand<Result>;
}
