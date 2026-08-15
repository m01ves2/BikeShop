using System.Collections.Generic;
using System.Linq;
using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Carts.DTOs;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Carts.MergeGuestCart
{
    public class MergeGuestCartCommandHandler : ICommandHandler<MergeGuestCartCommand, Result<MergeGuestCartResultDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        public MergeGuestCartCommandHandler(ICustomerRepository customerRepository, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }

        public async Task<Result<MergeGuestCartResultDto>> Handle(MergeGuestCartCommand command, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerWithCartByApplicationUserIdAsync(command.ApplicationUserId, cancellationToken);

            if (customer == null)
                return Result<MergeGuestCartResultDto>.Failure(new Error(ErrorCode.NotFound, $"Not found customer with applicationUserId = {command.ApplicationUserId}"));

            //1. ищем совпадения гостевой корзины с серверной и корректируем количество
            var cart = customer.Cart;
            var guestCart = command.GuestCart;
            var guestItemsByProductId = guestCart.Items.ToDictionary(item => item.Product.Id); //Dictionary<ProductId, GuestCartItem>

            foreach (var item in cart.Items) {
                if (guestItemsByProductId.TryGetValue(item.ProductId, out var guestItem)) {
                    item.ChangeQuantity(Math.Max(item.Quantity, guestItem.Quantity));

                    guestItemsByProductId.Remove(item.ProductId);
                }
            }

            //2. остаток проверяем на наличие такого продукта в базе, и если есть - добавляем в серверную корзину
            var guestOnlyProductIds = guestItemsByProductId.Keys.ToList(); //тут остались только те items, которых не было в серверной корзине
            var existingProducts = await _productRepository.GetByIdsAsync(guestOnlyProductIds, cancellationToken);

            foreach (var product in existingProducts) {
                cart.AddItem(product.Id, guestItemsByProductId[product.Id].Quantity);

                guestItemsByProductId.Remove(product.Id);
            }

            //3. остаток(продукт не существует в базе) - просто отдаём названиия удалённых продуктов в интерфейс
            var guestCartProductNamesNotExisting = guestItemsByProductId.Values.Select(i => i.Product.Name);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<MergeGuestCartResultDto>.Success(new MergeGuestCartResultDto(guestCartProductNamesNotExisting.ToList()));
        }
    }
}
