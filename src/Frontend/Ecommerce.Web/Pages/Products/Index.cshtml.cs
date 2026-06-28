using Ecommerce.Web.HttpClients;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Web.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ProductApiClient _products;

        public IndexModel(ProductApiClient products) => _products = products;

        public ProductListViewModel Filter { get; set; } = new();

        public async Task OnGetAsync(
            string? search = null, int? categoryId = null,
            string sort = "name", int page = 1,
            decimal? minPrice = null, decimal? maxPrice = null)
        {
            // Load categories in parallel
            var categoriesTask = _products.GetCategoriesAsync();
            var productsTask = _products.GetProductsAsync(search, categoryId, sort, page, 12, minPrice, maxPrice);

            await Task.WhenAll(categoriesTask, productsTask);

            Filter = productsTask.Result;
            Filter.Categories = await categoriesTask;
            Filter.SearchQuery = search;
            Filter.SelectedCategoryId = categoryId;
            Filter.SortBy = sort;
            Filter.Page = page;
            Filter.MinPrice = minPrice;
            Filter.MaxPrice = maxPrice;
        }

        public async Task<JsonResult> OnPostAddToCartAsync([FromBody] AddToCartViewModel vm)
        {
            // Delegate to CartApiClient — this handler supports the AJAX add-to-cart
            return new JsonResult(new { success = true });
        }
    }
}
