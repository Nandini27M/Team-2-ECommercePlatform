using Ecommerce.Web.ViewModels;
using System.Text.Json;

namespace Ecommerce.Web.HttpClients
{
    public class CartApiClient : BaseApiClient
    {
        public CartApiClient(HttpClient http, IHttpContextAccessor ctx) : base(http, ctx) { }

        public async Task<CartViewModel?> GetCartAsync()
        {
            AttachToken();
            try
            {
                var res = await _http.GetStringAsync("/api/cart");
                return JsonSerializer.Deserialize<CartViewModel>(res, _json);
            }
            catch { return null; }
        }

        public async Task<bool> AddToCartAsync(int productId, int quantity = 1)
        {
            AttachToken();
            try
            {
                var res = await _http.PostAsync("/api/cart/items", Json(new { productId, quantity }));
                return res.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        public async Task<bool> UpdateQuantityAsync(int cartItemId, int quantity)
        {
            AttachToken();
            try
            {
                var res = await _http.PutAsync($"/api/cart/items/{cartItemId}", Json(new { quantity }));
                return res.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        public async Task<bool> RemoveItemAsync(int cartItemId)
        {
            AttachToken();
            try
            {
                var res = await _http.DeleteAsync($"/api/cart/items/{cartItemId}");
                return res.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        public async Task<bool> ClearCartAsync()
        {
            AttachToken();
            try
            {
                var res = await _http.DeleteAsync("/api/cart");
                return res.IsSuccessStatusCode;
            }
            catch { return false; }
        }
    }
}
