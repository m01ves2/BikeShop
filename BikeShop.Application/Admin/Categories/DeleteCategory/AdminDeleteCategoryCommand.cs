using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Admin.Categories.DeleteCategory
{
    public sealed record AdminDeleteCategoryCommand(int Id) : ICommand<Result>;
}
