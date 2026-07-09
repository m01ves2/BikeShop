using BikeShop.Domain.Exceptions;

namespace BikeShop.Domain.Entities
{
    public class Product
    {
        private const int MaxNameLength = 30;
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }
        public Category Category { get; private set; }

        public Product(string name, Category category, decimal price, string description = "", int stockQuantity = 0)
        {
            ChangeName(name);
            ChangeCategory(category);
            ChangePrice(price);
            ChangeDescription(description);
            ChangeStockQuantity(stockQuantity);
        }

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) {
                throw new DomainValidationException("Name cannot be empty");
            }

            name = name.Trim();
            if(name.Length > MaxNameLength)
                throw new DomainValidationException($"Name cannot be longer than {MaxNameLength} chars");

            Name = name;
        }

        public void ChangeDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description)) {
                Description = "";
                return;
            }
            Description = description.Trim();
        }

        public void ChangePrice(decimal price)
        {
            if(price <= 0)
                throw new DomainValidationException("Price must be greater than 0");
            Price = price;
        }
        public void ChangeStockQuantity(int quantity)
        {
            if(quantity < 0)
                throw new DomainValidationException("Quantity must be greater or equal 0");
            StockQuantity = quantity;
        }

        public void ChangeCategory(Category category)
        {
            if (category == null)
                throw new DomainValidationException("Category cannot be null");

            Category = category;
        }
    }
}
