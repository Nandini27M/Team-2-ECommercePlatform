using Ecommerce.Web.HttpClients;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Web.Pages.Orders
{
    public class DetailModel : PageModel
    {
        private readonly OrderApiClient _orders;

        public DetailModel(OrderApiClient orders) => _orders = orders;

        public OrderDetailViewModel? Order { get; set; }

        public async Task OnGetAsync(string id)
        {
            Order = await _orders.GetOrderAsync(id);
        }
    }
}
