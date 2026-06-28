namespace Ecommerce.Web.ViewModels
{
    public class PaymentViewModel
    {
        public string PaymentMethod { get; set; } = "card";
        public string? CardNumber { get; set; }
        public string? CardHolder { get; set; }
        public string? ExpiryMonth { get; set; }
        public string? ExpiryYear { get; set; }
        public string? Cvv { get; set; }
        public string? UpiId { get; set; }
    }
}
