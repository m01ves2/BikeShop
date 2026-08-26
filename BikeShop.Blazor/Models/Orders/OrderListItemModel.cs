namespace BikeShop.Blazor.Models.Orders
{
    public sealed class OrderListItemModel
    {
        public int Id { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
