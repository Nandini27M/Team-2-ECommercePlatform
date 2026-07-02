using OrderService.DTOs;

namespace OrderService.Clients;

public class ProductServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public ProductServiceClient(HttpClient httpClient,
                                IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<ProductResponse?> GetProductAsync(int productId)
    {
        var baseUrl = _configuration["ServiceUrls:ProductService"];

        return await _httpClient.GetFromJsonAsync<ProductResponse>
        (
            $"{baseUrl}/api/Product/{productId}"
        );
    }
}