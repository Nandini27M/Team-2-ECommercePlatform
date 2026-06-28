using ProductService.Entities;

namespace ProductService.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync();

    Task<Product?> GetProductByIdAsync(int id);

    Task AddProductAsync(Product product);

    void UpdateProduct(Product product);

    void DeleteProduct(Product product);

    Task SaveChangesAsync();
}