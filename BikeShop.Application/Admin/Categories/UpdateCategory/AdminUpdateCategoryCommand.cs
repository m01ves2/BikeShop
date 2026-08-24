using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Admin.Categories.UpdateCategory
{
    public sealed record AdminUpdateCategoryCommand(int Id, string Name) : ICommand<Result>;
}
