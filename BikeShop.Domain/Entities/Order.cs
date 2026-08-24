using System.Runtime.Intrinsics.X86;
using BikeShop.Domain.Exceptions;

namespace BikeShop.Domain.Entities
{
    public enum OrderStatus
    {
        Pending = 0,
        Paid = 1,
        Shipped = 2,
        Completed = 3,
        Cancelled = 4,
    }

    public class Order
    {
        public int Id { get; private set; }

        public OrderStatus Status { get; private set; } = OrderStatus.Pending;

        //статическая структура данных в памяти, описывающая бизнес-правило.
        //она не является public Dictionary<...> AllowedTransitions { get; private set; }
        //то есть не является property Order, которую EF Core мог бы попытаться сохранить.
        //EF Core интересует примерно вот это: { get; private set;}. Вот эти данные сохраняются в БД.
        //поэтому можно прям тут сделать:
        private static readonly Dictionary<OrderStatus, OrderStatus[]> AllowedTransitions = new() {
            [OrderStatus.Pending] = new[]{ OrderStatus.Paid, OrderStatus.Cancelled },
            [OrderStatus.Paid] = new[]{ OrderStatus.Shipped, OrderStatus.Cancelled },
            [OrderStatus.Shipped] = new[]{ OrderStatus.Completed },
            [OrderStatus.Completed] = Array.Empty<OrderStatus>(),
            [OrderStatus.Cancelled] = Array.Empty<OrderStatus>()
        }; //это пример domain knowledge, которое существует только в коде!

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

            ChangeDeliveryAddress(deliveryAddress);
            ChangeDeliveryAt(deliveryAt);
            ChangeCustomerPhone(customerPhone);

            CreatedAt = DateTime.UtcNow;

        }

        public void ChangeStatus(OrderStatus status)
        {
            if (!AllowedTransitions[Status].Contains(status)) {
                throw new DomainValidationException($"Cannot change order status from {Status} to {status}");
            }

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

        public IReadOnlyCollection<OrderStatus> GetAllowedStatuses()
        {
            return AllowedTransitions[Status];
        }
    }
}