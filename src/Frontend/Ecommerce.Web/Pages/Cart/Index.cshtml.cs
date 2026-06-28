using Ecommerce.Web.HttpClients;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Web.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly CartApiClient _cart;

        public IndexModel(CartApiClient cart) => _cart = cart;

        public CartViewModel? Cart { get; set; }

        public async Task OnGetAsync()
        {
            Cart = await _cart.GetCartAsync();
            ViewData["CartCount"] = Cart?.TotalItems ?? 0;
        }

        // AJAX endpoint – Add item
        public async Task<JsonResult> OnPostAddItemAsync([FromBody] AddToCartViewModel vm)
        {
            var ok = await _cart.AddToCartAsync(vm.ProductId, vm.Quantity);
            return new JsonResult(new { success = ok });
        }

        // AJAX endpoint – Update quantity
        public async Task<JsonResult> OnPostUpdateItemAsync([FromBody] UpdateQtyRequest req)
        {
            var ok = await _cart.UpdateQuantityAsync(req.CartItemId, req.Quantity);
            return new JsonResult(new { success = ok });
        }

        // AJAX endpoint – Remove item
        public async Task<JsonResult> OnDeleteRemoveItemAsync(int itemId)
        {
            var ok = await _cart.RemoveItemAsync(itemId);
            return new JsonResult(new { success = ok });
        }

        // AJAX endpoint – Clear cart
        public async Task<JsonResult> OnDeleteClearAsync()
        {
            var ok = await _cart.ClearCartAsync();
            return new JsonResult(new { success = ok });
        }
    }

    public record UpdateQtyRequest(int CartItemId, int Quantity);

}
