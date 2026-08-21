using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Common.Models;
using BikeShop.Application.Orders.DTOs;
using BikeShop.Domain.Entities;

namespace BikeShop.Application.Orders.CreateOrder
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Result<CreateOrderResultDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public async Task<Result<CreateOrderResultDto>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerWithCartByApplicationUserIdAsync(command.ApplicationUserId, cancellationToken);

            if (customer == null) {
                return Result<CreateOrderResultDto>.Failure(new Error(ErrorCode.NotFound, $"Not found customer with applicationUserId = {command.ApplicationUserId}"));
            }

            var cart = customer.Cart;
            var orderItems = new List<OrderItem>();
            var orderItemAdjustments = new List<string>();

            foreach (var item in cart.Items) {
                var stock = item.Product.StockQuantity;

                if (stock <= 0) {
                    orderItemAdjustments.Add($"Product {item.Product.Name} is out of stock");
                    continue;
                }

                if (stock < item.Quantity) {
                    orderItemAdjustments.Add($"Product {item.Product.Name}: requested {item.Quantity}, but only {stock} were available. Quantity was adjusted to {stock}");
                }
                var quantity = Math.Min(item.Quantity, stock);

                item.Product.ChangeStockQuantity(stock - quantity);

                var orderItem = new OrderItem(item.ProductId, item.Product.Name, item.Product.Price, quantity);

                orderItems.Add(orderItem);
            }


            var order = new Order(customer, command.DeliveryAddress, command.DeliveryAt, command.CustomerPhone, orderItems);

            if (!order.Items.Any())
                return Result<CreateOrderResultDto>.Failure(new Error( ErrorCode.Conflict, "None of the products in your cart are currently available."));

            await _orderRepository.AddAsync(order, cancellationToken);

            cart.Clear();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<CreateOrderResultDto>.Success(new CreateOrderResultDto(orderItemAdjustments));
        }
    }
}
