namespace Ecommerce.Web.ViewModels
{
    public class OrderSummaryViewModel
    {
        public string OrderId { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public int ItemCount { get; set; }
        public string? TrackingNumber { get; set; }
        public string StatusBadgeClass => Status.ToLower() switch
        {
            "pending" => "bg-warning text-dark",
            "processing" => "bg-info text-dark",
            "shipped" => "bg-primary",
            "delivered" => "bg-success",
            "cancelled" => "bg-danger",
            _ => "bg-secondary"
        };
    }
}
