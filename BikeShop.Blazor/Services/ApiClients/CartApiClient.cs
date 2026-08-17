using System.Reflection;
using BikeShop.Blazor.Identity;
using BikeShop.Blazor.Models;
using BikeShop.Blazor.Services.Models;

namespace BikeShop.Blazor.Services.ApiClients
{
    public class CartApiClient : BaseApiClient
    {
        public CartApiClient(IHttpClientFactory factory, JwtAuthenticationStateProvider authStateProvider) : base(factory, authStateProvider)
        {
        }

        public async Task<CartModel?> GetCartAsync()
        {
            AddAuthorizationHeader();

            var url = $"api/cart";
            return await _http.GetFromJsonAsync<CartModel>(url);
        }

        ////POST
        //public async Task AddItemAsync(int productId)
        //{
        //    AddAuthorizationHeader();

        //    //var url = $"api/cart/";
        //    //return await _http.GetFromJsonAsync<List<CategoryListItemModel>>(url) ?? [];


        //    var response = await _http.PostAsJsonAsync("api/auth/register", productId);

        //    //if (response.IsSuccessStatusCode) {
        //    //    return null;
        //    //}

        //    //return await response.Content.ReadFromJsonAsync<ApiErrorResponseModel>();
        //}

        public async Task<SynchronizeCartResultModel?> SynchronizeCartAsync(CartModel localCart)
        {
            AddAuthorizationHeader();

            var response = await _http.PostAsJsonAsync("api/cart/synchronize", localCart);
            return await response.Content.ReadFromJsonAsync<SynchronizeCartResultModel>();
        }
    }
}
