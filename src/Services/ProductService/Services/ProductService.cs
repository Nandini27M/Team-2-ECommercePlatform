using ProductService.DTOs;
using ProductService.Entities;
using ProductService.Interfaces;

namespace ProductService.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync()
    {
        var products = await _repository.GetAllProductsAsync();

        return products.Select(p => new ProductResponse
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Stock = p.Stock,
            Category = p.Category
        });
    }

    public async Task<ProductResponse?> GetProductByIdAsync(int id)
    {
        var product = await _repository.GetProductByIdAsync(id);

        if (product == null)
            return null;

        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            Category = product.Category
        };
    }

    public async Task<ApiResponse> AddProductAsync(ProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Stock = request.Stock,
            Category = request.Category
        };

        await _repository.AddProductAsync(product);
        await _repository.SaveChangesAsync();

        return new ApiResponse
        {
            Success = true,
            Message = "Product added successfully."
        };
    }

    public async Task<ApiResponse> UpdateProductAsync(int id, ProductRequest request)
    {
        var product = await _repository.GetProductByIdAsync(id);

        if (product == null)
        {
            return new ApiResponse
            {
                Success = false,
                Message = "Product not found."
            };
        }

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.Stock = request.Stock;
        product.Category = request.Category;

        _repository.UpdateProduct(product);
        await _repository.SaveChangesAsync();

        return new ApiResponse
        {
            Success = true,
            Message = "Product updated successfully."
        };
    }

    public async Task<ApiResponse> DeleteProductAsync(int id)
    {
        var product = await _repository.GetProductByIdAsync(id);

        if (product == null)
        {
            return new ApiResponse
            {
                Success = false,
                Message = "Product not found."
            };
        }

        _repository.DeleteProduct(product);
        await _repository.SaveChangesAsync();

        return new ApiResponse
        {
            Success = true,
            Message = "Product deleted successfully."
        };
    }
}