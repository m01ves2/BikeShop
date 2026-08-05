using System.Security.Claims;
using BikeShop.Blazor.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace BikeShop.Blazor.Identity
{
    public sealed class JwtAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _currentUser = Anonymous().User;

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
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(token);

            _currentUser = new ClaimsPrincipal(
                new ClaimsIdentity(
                    jwt.Claims,
                    authenticationType: "jwt"));

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void SignOut()
        {
            _currentUser = Anonymous().User;

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
