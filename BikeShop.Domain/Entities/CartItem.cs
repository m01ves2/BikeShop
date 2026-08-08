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

        public CartItem(Product product, int quantity)
        {
            if (product == null)
                throw new DomainValidationException("Product cannot be null");

            Product = product;

            ChangeQuantity(quantity);
        }


        internal void IncreaseQuantity(int amount)
        {
            if (amount <= 0)
                throw new DomainValidationException("Amount must be greater than zero");

            Quantity += amount;
        }

        internal void DecreaseQuantity(int amount)
        {
            if (amount <= 0)
                throw new DomainValidationException("Amount must be greater than zero");

            Quantity -= amount;
        }

        private void ChangeQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new DomainValidationException("Quantity must be greater than zero");

            Quantity = quantity; 
        }
    }
}
