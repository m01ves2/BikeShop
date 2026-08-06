using System.Security.Claims;
using BikeShop.Blazor.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace BikeShop.Blazor.Identity
{
    public sealed class JwtAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _currentUser = Anonymous().User;
        private string? _token;
        public string? Token => _token;

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(_currentUser));
        }

        private static AuthenticationState Anonymous()
        {
            return new AuthenticationState(
                new ClaimsPrincipal(
                    new ClaimsIdentity()));
        }

        public void SignIn(string token)
        {
            _token = token;

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            _currentUser = new ClaimsPrincipal(
                new ClaimsIdentity(jwt.Claims, "jwt"));

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

            //Console.WriteLine(token);
        }

        public void SignOut()
        {
            _token = null;

            _currentUser = Anonymous().User;

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
