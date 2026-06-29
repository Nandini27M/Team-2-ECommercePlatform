namespace ShippingService.DTOs
{
    public class CreateShippingRequest
    {
        public Guid OrderId { get; set; }

        public string TransactionId { get; set; } = string.Empty;

        public string CustomerEmail { get; set; } = string.Empty;

        public string ShippingAddress { get; set; } = string.Empty;
    }
}
