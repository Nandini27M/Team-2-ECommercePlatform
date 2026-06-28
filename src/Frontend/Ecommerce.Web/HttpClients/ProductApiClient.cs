using Ecommerce.Web.ViewModels;
using System.Text.Json;

namespace Ecommerce.Web.HttpClients
{
    public class ProductApiClient : BaseApiClient
    {
        public ProductApiClient(HttpClient http, IHttpContextAccessor ctx) : base(http, ctx) { }

        public async Task<ProductListViewModel> GetProductsAsync(
            string? search = null, int? categoryId = null,
            string sort = "name", int page = 1, int pageSize = 12,
            decimal? minPrice = null, decimal? maxPrice = null)
        {
            AttachToken();
            var qs = $"?page={page}&pageSize={pageSize}&sort={sort}";
            if (!string.IsNullOrEmpty(search)) qs += $"&search={Uri.EscapeDataString(search)}";
            if (categoryId.HasValue) qs += $"&categoryId={categoryId}";
            if (minPrice.HasValue) qs += $"&minPrice={minPrice}";
            if (maxPrice.HasValue) qs += $"&maxPrice={maxPrice}";

            try
            {
                var res = await _http.GetStringAsync($"/api/products{qs}");
                return JsonSerializer.Deserialize<ProductListViewModel>(res, _json) ?? new();
            }
            catch { return new(); }
        }

        public async Task<ProductDetailViewModel?> GetProductAsync(int id)
        {
            AttachToken();
            try
            {
                var res = await _http.GetStringAsync($"/api/products/{id}");
                return JsonSerializer.Deserialize<ProductDetailViewModel>(res, _json);
            }
            catch { return null; }
        }

        public async Task<List<CategoryViewModel>> GetCategoriesAsync()
        {
            try
            {
                var res = await _http.GetStringAsync("/api/products/categories");
                return JsonSerializer.Deserialize<List<CategoryViewModel>>(res, _json) ?? new();
            }
            catch { return new(); }
        }
    }
}
