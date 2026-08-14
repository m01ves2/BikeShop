namespace BikeShop.Blazor.Models
{
    //public class CartItemModel
    //{
    //    public int ProductId { get; set; }
    //    public string ProductName { get; set; } = string.Empty;
    //    public decimal Price { get; set; }
    //    public int Quantity { get; set; }
    //    public string? ImageUrl { get; set; }
    //    public decimal Total => Price * Quantity;
    //}
    //public sealed record CartItemModel(int Id, int Quantity, ProductCartModel Product);

    public class CartItemModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public ProductCartModel Product { get; set; } = null!;

        public decimal Total => Product.Price * Quantity;
    }
}
