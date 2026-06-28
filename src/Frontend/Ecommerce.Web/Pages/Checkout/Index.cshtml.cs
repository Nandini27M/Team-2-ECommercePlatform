using Ecommerce.Web.HttpClients;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Web.Pages.Checkout
{
    public class IndexModel : PageModel
    {
        private readonly CartApiClient _cart;
        private readonly OrderApiClient _orders;

        public IndexModel(CartApiClient cart, OrderApiClient orders)
        {
            _cart = cart;
            _orders = orders;
        }

        [BindProperty]
        public CheckoutViewModel Checkout { get; set; } = new();

        public List<string> IndianStates { get; } = new()
        {
            "Andhra Pradesh","Arunachal Pradesh","Assam","Bihar","Chhattisgarh","Goa","Gujarat","Haryana","Himachal Pradesh","Jharkhand","Karnataka","Kerala","Madhya Pradesh",
            "Maharashtra","Manipur","Meghalaya","Mizoram","Nagaland","Odisha","Punjab","Rajasthan","Sikkim","Tamil Nadu","Telangana","Tripura","Uttar Pradesh",
            "Uttarakhand","West Bengal","Delhi","Jammu & Kashmir","Ladakh"
    };

        public async Task<IActionResult> OnGetAsync()
        {
            var cart = await _cart.GetCartAsync();
            if (cart is null || !cart.Items.Any())
                return RedirectToPage("/Cart/Index");

            Checkout.Cart = cart;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Reload cart for display even if validation fails
            Checkout.Cart = await _cart.GetCartAsync() ?? new();

            if (!ModelState.IsValid) return Page();

            var (success, orderId, error) = await _orders.PlaceOrderAsync(Checkout);

            if (!success)
            {
                ModelState.AddModelError(string.Empty, error ?? "Failed to place order.");
                return Page();
            }

            TempData["SuccessMessage"] = "Order placed successfully! 🎉";
            return RedirectToPage("/Orders/Confirmation", new { orderId });
        }
    }
}
