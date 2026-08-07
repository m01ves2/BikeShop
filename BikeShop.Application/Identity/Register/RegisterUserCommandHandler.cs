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

        public RegisterUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            return await _userService.RegisterAsync(command.Email, command.Password, cancellationToken);

            //var result = await _userService.RegisterAsync(...);

            //if (result.IsFailure)
            //    return Result.Failure(result.Error);

            //var customer = new Customer(result.Value);

            //await _customerRepository.AddAsync(customer, cancellationToken);

            //await _unitOfWork.SaveChangesAsync(cancellationToken);

            //return Result.Success();
        }
    }
}
