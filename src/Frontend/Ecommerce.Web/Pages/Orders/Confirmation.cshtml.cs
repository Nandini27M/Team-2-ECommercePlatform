using Ecommerce.Web.HttpClients;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Web.Pages.Orders
{
    public class ConfirmationModel : PageModel
    {
        private readonly OrderApiClient _orders;

        public ConfirmationModel(OrderApiClient orders) => _orders = orders;

        public OrderDetailViewModel? Order { get; set; }

        public async Task<IActionResult> OnGetAsync(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
                return RedirectToPage("/Orders/Index");
            Order = await _orders.GetOrderAsync(orderId);
            return Page();
        }
    }
}
