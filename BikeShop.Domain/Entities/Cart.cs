using BikeShop.Domain.Exceptions;

namespace BikeShop.Domain.Entities
{
    public class Cart
    {
        public int Id { get; private set; }
        public int CustomerId { get; private set; }

        private readonly List<CartItem> _items = new();

        public IReadOnlyCollection<CartItem> Items => _items;


        public void AddItem(Product product, int amount)
        {
            if (amount <= 0)
                throw new DomainValidationException("Amount must be greater than zero");

            var existing = _items.FirstOrDefault(x => x.ProductId == product.Id);

            if (existing != null) {
                existing.IncreaseQuantity(amount);
                return;
            }

            _items.Add(new CartItem(product, amount));
        }

        public void RemoveItem(Product product)
        {
            var item = _items.FirstOrDefault(x => x.ProductId == product.Id);

            if (item == null)
                return;

            _items.Remove(item);
        }

        public void IncreaseQuantity(Product product, int amount)
        {
            if (amount <= 0)
                throw new DomainValidationException("Amount must be greater than zero");

            var item = _items.FirstOrDefault(x => x.ProductId == product.Id);
            if (item == null)
                return;

            item.IncreaseQuantity(amount);
        }

        public void DecreaseQuantity(Product product, int amount)
        {
            if (amount <= 0)
                throw new DomainValidationException("Amount must be greater than zero");

            var item = _items.FirstOrDefault(x => x.ProductId == product.Id);

            if (item == null)
                return;


            item.DecreaseQuantity(amount);
            
            if(item.Quantity <= 0)
                _items.Remove(item);
        }
    }
}
