using OrderService.DTOs;

namespace OrderService.Clients;

public class CartServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public CartServiceClient(
        HttpClient httpClient,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<List<CartResponse>?> GetCartItemsAsync(int userId)
    {
        var baseUrl = _configuration["ServiceUrls:CartService"];

        return await _httpClient.GetFromJsonAsync<List<CartResponse>>
        (
            $"{baseUrl}/api/cart/{userId}"
        );
    }
}