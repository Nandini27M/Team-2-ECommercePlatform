using Ecommerce.Web.HttpClients;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Web.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly OrderApiClient _orders;

        public IndexModel(OrderApiClient orders) => _orders = orders;

        public OrderHistoryViewModel Orders { get; set; } = new();

        public async Task OnGetAsync(int page = 1, string? filterStatus = null)
        {
            Orders = await _orders.GetOrdersAsync(page, filterStatus);
            Orders.FilterStatus = filterStatus;
            Orders.Page = page;
        }

        public async Task<JsonResult> OnPostCancelAsync(string orderId)
        {
            var ok = await _orders.CancelOrderAsync(orderId);
            return new JsonResult(new { success = ok });
        }
    }
}
