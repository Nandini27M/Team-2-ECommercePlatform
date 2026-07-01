using ProductService.DTOs;

namespace ProductService.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductResponse>> GetAllProductsAsync();

    Task<ProductResponse?> GetProductByIdAsync(int id);

    Task<ApiResponse> AddProductAsync(ProductRequest request);

    Task<ApiResponse> UpdateProductAsync(int id, ProductRequest request);

    Task<ApiResponse> DeleteProductAsync(int id);
}