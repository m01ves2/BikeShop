using BikeShop.Blazor.Models;

namespace BikeShop.Blazor.Services
{
    public class BreadcrumbService
    {
        public IReadOnlyList<BreadcrumbItem> Items { get; private set; } = [];
        public event Action? Changed;

        public void SetItems(IEnumerable<BreadcrumbItem> items)
        {
            Items = items.ToList();
            Changed?.Invoke();
        }
    }
}
