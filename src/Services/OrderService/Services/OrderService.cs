using System.Net.Http.Json;
using OrderService.DTOs;
using OrderService.Entities;
using OrderService.Interfaces;

namespace OrderService.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly IOrderPublisher _orderPublisher;

    public OrderService(
        IOrderRepository repository,
        HttpClient httpClient,
        IConfiguration configuration,
        IOrderPublisher orderPublisher)
    {
        _repository = repository;
        _httpClient = httpClient;
        _configuration = configuration;
        _orderPublisher = orderPublisher;
    }

    public async Task<ApiResponse> CreateOrderAsync(CreateOrderRequest request)
    {
        var cartUrl = _configuration["ServiceUrls:CartService"];
        var productUrl = _configuration["ServiceUrls:ProductService"];

        Console.WriteLine($"Cart URL: {cartUrl}/api/cart/{request.UserId}");

        var cartItems = await _httpClient.GetFromJsonAsync<List<CartResponse>>
        (
            $"{cartUrl}/api/cart/{request.UserId}"
        );

        if (cartItems == null || !cartItems.Any())
        {
            return new ApiResponse
            {
                Success = false,
                Message = "Cart is empty."
            };
        }

        var order = new Order
        {
            UserId = request.UserId,
            OrderDate = DateTime.UtcNow,
            Status = "Pending"
        };

        decimal totalAmount = 0;

        foreach (var item in cartItems)
        {
            Console.WriteLine($"Product URL: {productUrl}/api/Product/{item.ProductId}");

            var product = await _httpClient.GetFromJsonAsync<ProductResponse>
            (
                $"{productUrl}/api/Product/{item.ProductId}"
            );

            if (product == null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = $"Product {item.ProductId} not found."
                };
            }

            order.OrderItems.Add(new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = product.Price
            });

            totalAmount += product.Price * item.Quantity;
        }

        order.TotalAmount = totalAmount;

        await _repository.AddOrderAsync(order);
        await _repository.SaveChangesAsync();

        // Publish OrderCreated event
        await _orderPublisher.PublishOrderCreatedAsync(order);

        return new ApiResponse
        {
            Success = true,
            Message = "Order placed successfully."
        };
    }

    public async Task<OrderResponse?> GetOrderAsync(int orderId)
    {
        var order = await _repository.GetOrderByIdAsync(orderId);

        if (order == null)
            return null;

        return new OrderResponse
        {
            OrderId = order.OrderId,
            UserId = order.UserId,
            TotalAmount = order.TotalAmount,
            Status = order.Status,
            OrderDate = order.OrderDate
        };
    }

    public async Task<List<OrderResponse>> GetOrderHistoryAsync(int userId)
    {
        var orders = await _repository.GetOrdersByUserIdAsync(userId);

        return orders.Select(o => new OrderResponse
        {
            OrderId = o.OrderId,
            UserId = o.UserId,
            TotalAmount = o.TotalAmount,
            Status = o.Status,
            OrderDate = o.OrderDate
        }).ToList();
    }
}