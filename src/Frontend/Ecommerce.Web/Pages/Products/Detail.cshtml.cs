using Ecommerce.Web.HttpClients;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Web.Pages.Products
{
    public class DetailModel : PageModel
    {
        private readonly ProductApiClient _products;

        public DetailModel(ProductApiClient products) => _products = products;

        public ProductDetailViewModel? Product { get; set; }

        public async Task OnGetAsync(int id)
        {
            Product = await _products.GetProductAsync(id);
        }
    }
}
