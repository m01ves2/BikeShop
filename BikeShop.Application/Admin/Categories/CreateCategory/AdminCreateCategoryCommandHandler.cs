using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Common.Models;
using BikeShop.Domain.Entities;

namespace BikeShop.Application.Admin.Categories.CreateCategory
{
    public class AdminCreateCategoryCommandHandler : ICommandHandler<AdminCreateCategoryCommand, Result>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AdminCreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AdminCreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = new Category(command.Name);

            await _categoryRepository.AddAsync(category, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
