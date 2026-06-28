using Ecommerce.Web.ViewModels;
using System.Net.Http;
using System.Text.Json;

namespace Ecommerce.Web.HttpClients
{
    public class OrderApiClient : BaseApiClient
    {
        public OrderApiClient(HttpClient http, IHttpContextAccessor ctx) : base(http, ctx) { }

        public async Task<OrderHistoryViewModel> GetOrdersAsync(int page = 1, string? status = null)
        {
            AttachToken();
            try
            {
                var qs = $"?page={page}";
                if (!string.IsNullOrEmpty(status)) qs += $"&status={status}";
                var res = await _http.GetStringAsync($"/api/orders{qs}");
                return JsonSerializer.Deserialize<OrderHistoryViewModel>(res, _json) ?? new();
            }
            catch { return new(); }
        }

        public async Task<OrderDetailViewModel?> GetOrderAsync(string orderId)
        {
            AttachToken();
            try
            {
                var res = await _http.GetStringAsync($"/api/orders/{orderId}");
                return JsonSerializer.Deserialize<OrderDetailViewModel>(res, _json);
            }
            catch { return null; }
        }

        public async Task<(bool Success, string? OrderId, string? Error)> PlaceOrderAsync(CheckoutViewModel vm)
        {
            AttachToken();
            try
            {
                var res = await _http.PostAsync("/api/orders", Json(vm));
                var body = await res.Content.ReadAsStringAsync();
                if (!res.IsSuccessStatusCode)
                    return (false, null, JsonSerializer.Deserialize<OrderErrorResponse>(body, _json)?.Message ?? "Order failed");
                var data = JsonSerializer.Deserialize<OrderPlacedResponse>(body, _json);
                return (true, data?.OrderId, null);
            }
            catch { return (false, null, "Unable to place order"); }
        }

        public async Task<bool> CancelOrderAsync(string orderId)
        {
            AttachToken();
            try
            {
                var res = await _http.PostAsync($"/api/orders/{orderId}/cancel", new StringContent(""));
                return res.IsSuccessStatusCode;
            }
            catch { return false; }
        }
    }

    // ── Local DTOs used only by OrderApiClient ──
    internal record OrderErrorResponse(string Message);
    internal record OrderPlacedResponse(string OrderId);
}
