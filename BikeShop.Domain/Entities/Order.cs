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


        public DateTime DeliveryAt { get; private set; }
        public string DeliveryAddress { get; private set; }
        public string CustomerPhone { get; private set; } = string.Empty;
        public string? CourierPhone { get; private set; }


        public int CustomerId { get; private set; }
        public Customer Customer { get; private set; }


        private readonly List<OrderItem> _items = new();
        public IReadOnlyCollection<OrderItem> Items => _items;

        public decimal TotalPrice => _items.Sum(x => x.UnitPrice * x.Quantity);

        private Order()
        {
        }
        public Order( Customer customer, string deliveryAddress, DateTime deliveryAt,  string customerPhone, IEnumerable<OrderItem> items)
        {
            if (customer == null)
                throw new DomainValidationException("Customer cannot be empty");

            var orderItems = items?.ToList();

            if (orderItems == null || orderItems.Count == 0)
                throw new DomainValidationException("List of order items cannot be empty");

            Customer = customer;
            _items.AddRange(orderItems);

            ChangeStatus(OrderStatus.Pending);

            ChangeDeliveryAddress(deliveryAddress);
            ChangeDeliveryAt(deliveryAt);
            ChangeCustomerPhone(customerPhone);

            CreatedAt = DateTime.UtcNow;

        }

        public void ChangeStatus(OrderStatus status)
        {
            Status = status;
        }

        public void ChangeDeliveryAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new DomainValidationException("Delivery address cannot be empty");

            DeliveryAddress = address;
        }

        public void ChangeDeliveryAt(DateTime deliveryAt)
        {
            if (Status != OrderStatus.Pending &&
                Status != OrderStatus.Paid) {
                throw new DomainValidationException("Delivery time cannot be changed for the current order status");
            }

            var now = DateTime.UtcNow;

            if (deliveryAt < now || deliveryAt > now.AddDays(7)) {
                throw new DomainValidationException("Delivery date must be within the next 7 days");
            }

            DeliveryAt = deliveryAt;
        }
        public void ChangeCustomerPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                throw new DomainValidationException("Customer phone cannot be empty");

            CustomerPhone = phone;
        }

        public void AssignCourierPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                throw new DomainValidationException("Courier phone cannot be empty");

            CourierPhone = phone;
        }
    }
}
