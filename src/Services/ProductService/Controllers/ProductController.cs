using Microsoft.AspNetCore.Mvc;
using ProductService.DTOs;
using ProductService.Interfaces;

namespace ProductService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    // GET: api/Product
    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    // GET: api/Product/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    // POST: api/Product
    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductRequest request)
    {
        var result = await _productService.AddProductAsync(request);
        return Ok(result);
    }

    // PUT: api/Product/1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductRequest request)
    {
        var result = await _productService.UpdateProductAsync(id, request);

        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    // DELETE: api/Product/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var result = await _productService.DeleteProductAsync(id);

        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }
}