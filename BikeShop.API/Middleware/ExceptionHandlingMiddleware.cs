using BikeShop.Application.Common.Models;
using BikeShop.Domain.Exceptions;

namespace BikeShop.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try {
                await _next(context);
            }
            catch (DomainValidationException ex) {
                await HandleDomainValidationExceptionAsync(context, ex);
            }
            catch (Exception ex) {
                await HandleUnexpectedExceptionAsync(context, ex);
            }
        }

        //И вот здесь особенно красиво то, что мы используем тот же Error, который уже используется нашим собственным ErrorMapper!
        private async Task HandleDomainValidationExceptionAsync(HttpContext context, DomainValidationException exception)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            await context.Response.WriteAsJsonAsync(new Error(ErrorCode.Validation, exception.Message));
        }

        private async Task HandleUnexpectedExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "Unhandled exception while processing request");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsJsonAsync(new Error( ErrorCode.Unexpected, "Internal server error."));
        }
    }
}
