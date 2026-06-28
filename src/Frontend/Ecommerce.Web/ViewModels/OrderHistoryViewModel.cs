namespace Ecommerce.Web.ViewModels
{
    public class OrderHistoryViewModel
    {
        public List<OrderSummaryViewModel> Orders { get; set; } = new();
        public int Page { get; set; } = 1;
        public int TotalPages { get; set; }
        public string? FilterStatus { get; set; }
    }
}
