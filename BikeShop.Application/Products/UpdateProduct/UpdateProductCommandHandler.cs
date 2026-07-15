using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Products.UpdateProduct
{
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, Result>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IProductRepository productRepository,   ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(command.CategoryId, cancellationToken);
            if (category is null)
                return Result.Failure(new Error(ErrorCode.NotFound, $"Not found category id = {command.CategoryId}"));

            var product = await _productRepository.GetByIdAsync(command.Id, cancellationToken);

            if (product == null) {
                return Result.Failure(new Error(ErrorCode.NotFound, $"Not found product id = {command.Id}"));
            }

            product.ChangeName(command.Name);
            product.ChangeCategory(category);
            product.ChangeDescription(command.Description);
            product.ChangePrice(command.Price);
            product.ChangeStockQuantity(command.StockQuantity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
