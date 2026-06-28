namespace Ecommerce.Web.ViewModels
{
    public class OrderDetailViewModel
    {
        public string OrderId { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<OrderItemViewModel> Items { get; set; } = new();
        public ShippingAddressViewModel ShippingAddress { get; set; } = new();
        public decimal SubTotal { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string? TrackingNumber { get; set; }
        public List<OrderTrackingStep> TrackingSteps { get; set; } = new();
    }
}
