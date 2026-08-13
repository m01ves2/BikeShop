using System.Diagnostics;
using BikeShop.Domain.Exceptions;

namespace BikeShop.Domain.Entities
{
    public class CartItem
    {
        public int Id { get; private set; }
        public int Quantity { get; private set; }

        public int ProductId { get; private set; }
        public Product Product { get; private set; }

        public int CartId { get; private set; }
        public Cart Cart { get; private set; }

        
        private CartItem()
        {
        }

        public CartItem(int productId, int quantity)
        {
            if (productId <= 0)
                throw new DomainValidationException("ProductId must be greater than zero");

            ProductId = productId;
            ChangeQuantity(quantity);
        }


        public void IncreaseQuantity(int amount)
        {
            if (amount <= 0)
                throw new DomainValidationException("Quantity cannot be negative");

            Quantity += amount;
        }

        public void DecreaseQuantity(int amount)
        {
            if (amount <= 0)
                throw new DomainValidationException("Quantity cannot be negative");

            Quantity = Math.Max(0, Quantity - amount);
        }

        private void ChangeQuantity(int quantity)
        {
            if (quantity < 0)
                throw new DomainValidationException("Quantity cannot be negative");

            Quantity = quantity; 
        }
    }
}
