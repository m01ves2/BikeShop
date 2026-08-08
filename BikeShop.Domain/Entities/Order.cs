using BikeShop.Domain.Exceptions;

namespace BikeShop.Domain.Entities
{
    public enum OrderStatus
    {
        Pending,
        Paid,
        Shipped,
        Completed,
        Cancelled,
    }
    public class Order
    {
        public int Id { get; private set; }

        public OrderStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public int CustomerId { get; private set; }
        public Customer Customer { get; private set; }


        private readonly List<OrderItem> _items = new();
        public IReadOnlyCollection<OrderItem> Items => _items;

        public decimal TotalPrice => _items.Sum(x => x.UnitPrice * x.Quantity);


        Order(Customer customer, IEnumerable<OrderItem> items)
        {
            if (customer == null)
                throw new DomainValidationException("Customer cannot be empty");

            if(items == null || items.Count() == 0)
                throw new DomainValidationException("List of order items cannot be empty");

            Customer = customer;
            _items.AddRange(items);
        }

        public void ChangeStatus(OrderStatus status)
        {
            Status = status;
        }
    }
}
