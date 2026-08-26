using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BikeShop.Blazor.Services.ApiClients;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BikeShop.Blazor.Identity
{
    public sealed class JwtAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _currentUser = Anonymous().User;
        private string? _token;
        public string? Token => _token;
        private readonly ITokenStorage _tokenStorage;

        public JwtAuthenticationStateProvider(ITokenStorage tokenStorage)
        {
            _tokenStorage = tokenStorage;
        }

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

        public bool SignIn(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            if (jwt.ValidTo <= DateTime.UtcNow) {
                SignOut();
                return false;
            }

            _token = token;

            _currentUser = new ClaimsPrincipal(
                new ClaimsIdentity(jwt.Claims, "jwt"));

            NotifyAuthenticationStateChanged( GetAuthenticationStateAsync() );

            return true;
        }

        public void SignOut()
        {
            _token = null;

            _currentUser = Anonymous().User;

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        //делает полноценный пользовательский logout
        public async Task SignOutAsync()
        {
            await _tokenStorage.RemoveTokenAsync();

            _token = null;
            _currentUser = Anonymous().User;

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
