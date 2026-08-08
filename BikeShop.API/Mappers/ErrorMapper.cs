using BikeShop.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace BikeShop.API.Mappers
{
    public static class ErrorMapper
    {
        public static IActionResult ToActionResult(this ControllerBase controller, Error error)
        {
            return error.Code switch
            {
                ErrorCode.Validation => controller.BadRequest(error),

                ErrorCode.NotFound => controller.NotFound(error),

                ErrorCode.Conflict => controller.Conflict(error),

                ErrorCode.Unauthorized => controller.Unauthorized(error),

                ErrorCode.Forbidden => controller.StatusCode(StatusCodes.Status403Forbidden, error),

                _ => controller.StatusCode(StatusCodes.Status500InternalServerError, error)
            };
        }
    }
}