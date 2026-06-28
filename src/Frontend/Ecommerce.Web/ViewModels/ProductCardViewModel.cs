namespace Ecommerce.Web.ViewModels
{
    public class ProductCardViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public double Rating { get; set; }
        public int ReviewCount { get; set; }
        public bool IsInStock => StockQuantity > 0;
        public bool IsLowStock => StockQuantity is > 0 and <= 5;
    }
}
