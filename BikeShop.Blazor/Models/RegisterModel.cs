namespace BikeShop.Blazor.Models
{
    public sealed class RegisterModel
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
