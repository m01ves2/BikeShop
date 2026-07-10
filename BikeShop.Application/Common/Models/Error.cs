namespace BikeShop.Application.Common.Models
{

    public enum ErrorCode
    {
        None,
        Validation,
        NotFound,
        Conflict,
        Unauthorized,
        Forbidden,
        Unexpected
    }

    public sealed record Error(ErrorCode Code, string Message);
}
