using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Carts.DTOs;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Carts.SynchronizeCart
{
    public class SynchronizeCartCommandHandler : ICommandHandler<SynchronizeCartCommand, Result<SynchronizeCartResultDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        public SynchronizeCartCommandHandler(ICustomerRepository customerRepository, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }

        public async Task<Result<SynchronizeCartResultDto>> Handle(SynchronizeCartCommand command, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerWithCartByApplicationUserIdAsync(command.ApplicationUserId, cancellationToken);

            if (customer == null)
                return Result<SynchronizeCartResultDto>.Failure(new Error(ErrorCode.NotFound, $"Not found customer with applicationUserId = {command.ApplicationUserId}"));

            //1. ищем совпадения гостевой корзины с серверной и корректируем количество
            var cart = customer.Cart;
            var localCart = command.LocalCart;
            var localCartItemsByProductId = localCart.Items.ToDictionary(item => item.Product.Id); //Dictionary<ProductId, localCartItem>

            foreach (var item in cart.Items) {
                if (localCartItemsByProductId.TryGetValue(item.ProductId, out var localCartItem)) {
                    item.ChangeQuantity(Math.Max(item.Quantity, localCartItem.Quantity));

                    localCartItemsByProductId.Remove(item.ProductId);
                }
            }

            //2. остаток проверяем на наличие такого продукта в базе, и если есть - добавляем в серверную корзину
            var localCartOnlyProductIds = localCartItemsByProductId.Keys.ToList(); //тут остались только те items, которых не было в серверной корзине
            var existingProducts = await _productRepository.GetByIdsAsync(localCartOnlyProductIds, cancellationToken);

            foreach (var product in existingProducts) {
                cart.AddItem(product.Id, localCartItemsByProductId[product.Id].Quantity);

                localCartItemsByProductId.Remove(product.Id);
            }

            //3. остаток(продукт не существует в базе) - просто отдаём названиия удалённых продуктов в интерфейс
            var removedProductNames = localCartItemsByProductId.Values.Select(i => i.Product.Name).ToList();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var cartDto = new CartDto(
                cart.Items.Select(i => new CartItemDto(
                    i.Id,
                    i.Quantity,
                    new ProductCartDto(
                        i.Product.Id,
                        i.Product.Name,
                        i.Product.Price,
                        i.Product.StockQuantity
                    ))).ToList()
                );

            return Result<SynchronizeCartResultDto>.Success( new SynchronizeCartResultDto(cartDto, removedProductNames));
        }
    }
}
