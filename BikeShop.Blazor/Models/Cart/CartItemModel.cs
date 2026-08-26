namespace BikeShop.Blazor.Models.Cart
{
    public class CartItemModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public ProductCartModel Product { get; set; } = null!;

        public decimal Total => Product.Price * Quantity;
    }
}
