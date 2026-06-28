namespace Ecommerce.Web.ViewModels
{
    public class ProductListViewModel
    {
        public List<ProductCardViewModel> Products { get; set; } = new();
        public List<CategoryViewModel> Categories { get; set; } = new();
        public string? SearchQuery { get; set; }
        public int? SelectedCategoryId { get; set; }
        public string SortBy { get; set; } = "name";
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 12;
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
