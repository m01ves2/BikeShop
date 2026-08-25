using BikeShop.Application.Abstractions.Identity;
using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Common.Models;
using BikeShop.Domain.Entities;

namespace BikeShop.Application.Admin.Identity.Register
{
    public class RegisterAdminCommandHandler : ICommandHandler<RegisterAdminCommand, Result>
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICartRepository _cartRepository;

        public RegisterAdminCommandHandler(IUserService userService, IUnitOfWork unitOfWork, ICustomerRepository customerRepository, ICartRepository cartRepository)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
            _cartRepository = cartRepository;
        }

        public async Task<Result> Handle(RegisterAdminCommand command, CancellationToken cancellationToken)
        {
            var result = await _userService.RegisterAsync(command.Email, command.Password, ["Customer", "Admin"], cancellationToken);

            if (result.IsFailure)
                return Result.Failure(result.Error);

            var customer = new Customer(result.Data);
            await _customerRepository.AddAsync(customer, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var cart = new Cart(customer.Id); //вот тут мы передаём только Id, а значит, перед этим придется вызвать SaveChangesAsync, чтобы его сгенерировать
            await _cartRepository.AddAsync(cart, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
