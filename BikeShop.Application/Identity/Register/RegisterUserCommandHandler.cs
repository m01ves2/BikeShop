using BikeShop.Application.Abstractions.Identity;
using BikeShop.Application.Abstractions.Messaging;
using BikeShop.Application.Abstractions.Persistence;
using BikeShop.Application.Common.Models;
using BikeShop.Domain.Entities;

namespace BikeShop.Application.Authentication.Register
{
    public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Result>
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;

        public RegisterUserCommandHandler(IUserService userService, IUnitOfWork unitOfWork, ICustomerRepository customerRepository)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
        }

        public async Task<Result> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            //return await _userService.RegisterAsync(command.Email, command.Password, cancellationToken);

            var result = await _userService.RegisterAsync(command.Email, command.Password, cancellationToken);

            if (result.IsFailure)
                return Result.Failure(result.Error);

            var customer = new Customer(result.Data);

            await _customerRepository.AddAsync(customer, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
