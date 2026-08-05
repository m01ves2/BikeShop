using Microsoft.JSInterop;

namespace BikeShop.Blazor.Services
{
    public class TokenStorage : ITokenStorage
    {
        private readonly IJSRuntime _jsRuntime;

        public TokenStorage(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task SaveTokenAsync(string token)
        {
            await _jsRuntime.InvokeVoidAsync("authStorage.saveToken", token);
        }

        public async Task<string?> GetTokenAsync()
        {
            return await _jsRuntime.InvokeAsync<string?>("authStorage.getToken");
        }

        public async Task RemoveTokenAsync()
        {
            await _jsRuntime.InvokeVoidAsync("authStorage.removeToken");
        }
    }
}
