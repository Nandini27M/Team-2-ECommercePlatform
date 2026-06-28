namespace Ecommerce.Web.ViewModels
{
    public class CheckoutViewModel
    {
    public CartViewModel Cart { get; set; } = new();
    public ShippingAddressViewModel ShippingAddress { get; set; } = new();
    public PaymentViewModel Payment { get; set; } = new();
    public string? CouponCode { get; set; }
    public decimal Discount { get; set; }
    }
}
