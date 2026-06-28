namespace Ecommerce.Web.ViewModels
{
    public class ProductDetailViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public List<string> ImageGallery { get; set; } = new();
        public string CategoryName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int StockQuantity { get; set; }
        public bool IsInStock => StockQuantity > 0;
        public bool IsLowStock => StockQuantity is > 0 and <= 5;
        public double Rating { get; set; }
        public int ReviewCount { get; set; }
        public List<ProductReviewViewModel> Reviews { get; set; } = new();
        public List<ProductCardViewModel> RelatedProducts { get; set; } = new();
        public Dictionary<string, string> Specifications { get; set; } = new();
    }
}
