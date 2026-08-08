using BikeShop.Domain.Exceptions;

namespace BikeShop.Domain.Entities
{
    public class Cart
    {
        public int Id { get; private set; }

        public int CustomerId { get; private set; }
        public Customer Customer { get; private set; } = null!;


        //public List<CartItem> Items { get; private set; } //Но это защищает только замену самой коллекции: cart.Items = anotherList;
        //а мы хотим, чтобы в самой коллекцию контролировалось изменение элементов,
        //поэтому делаем так:
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
