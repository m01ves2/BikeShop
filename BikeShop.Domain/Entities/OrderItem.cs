using BikeShop.Domain.Exceptions;

namespace BikeShop.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; private set; }

        public string ProductName { get; private set; }

        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }

        public int OrderId { get; private set; }

        public int ProductId { get; private set; }

        public OrderItem(int productId, string productName, decimal unitPrice, int quantity)
        {
            if (productId <= 0)
                throw new DomainValidationException("Product ID must be greater than zero");

            if (string.IsNullOrWhiteSpace(productName))
                throw new DomainValidationException("Product name cannot be empty");

            if (unitPrice < 0)
                throw new DomainValidationException("Unit price cannot be negative");

            if (quantity <= 0)
                throw new DomainValidationException("Quantity must be greater than zero");

            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
    }
}
