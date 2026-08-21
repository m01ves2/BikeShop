using System.Security.Claims;
using BikeShop.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace BikeShop.API.Controllers
{
    public abstract class BikeShopController : ControllerBase
    {
        protected Result<int> GetApplicationUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userId, out var applicationUserId))
                return Result<int>.Failure(new Error(ErrorCode.Validation, "Invalid user identity"));

            return Result<int>.Success(applicationUserId);
        }
    }
}
