using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Entities;
using ProductService.Interfaces;

namespace ProductService.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task AddProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public void UpdateProduct(Product product)
    {
        _context.Products.Update(product);
    }

    public void DeleteProduct(Product product)
    {
        _context.Products.Remove(product);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}