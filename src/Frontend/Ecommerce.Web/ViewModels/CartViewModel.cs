namespace Ecommerce.Web.ViewModels
{
    public class CartViewModel
    {
        public string CartId { get; set; } = string.Empty;
        public List<CartItemViewModel> Items { get; set; } = new();
        public decimal SubTotal => Items.Sum(i => i.TotalPrice);
        public decimal ShippingCost => SubTotal >= 500 ? 0 : 50;
        public decimal Tax => Math.Round(SubTotal * 0.18m, 2);
        public decimal Total => SubTotal + ShippingCost + Tax;
        public int TotalItems => Items.Sum(i => i.Quantity);
        public bool IsFreeShipping => SubTotal >= 500;
    }
}
