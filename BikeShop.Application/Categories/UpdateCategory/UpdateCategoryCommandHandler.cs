using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Common.Models;

namespace BikeShop.Application.Categories.UpdateCategory
{
    internal class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand, Result>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(command.Id, cancellationToken);

            if (category == null) {
                return Result.Failure(new Error(ErrorCode.NotFound, $"Not found category id = {command.Id}"));
            }

            category.ChangeName(command.Name);            

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
