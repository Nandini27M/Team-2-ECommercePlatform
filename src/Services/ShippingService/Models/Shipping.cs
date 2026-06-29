namespace ShippingService.Models
{
    public class Shipping
    {
        public int ShippingId { get; set; }

        public Guid OrderId { get; set; }

        public string TransactionId { get; set; } = string.Empty;

        public string CustomerEmail { get; set; } = string.Empty;

        public string ShippingAddress { get; set; } = string.Empty;

        public string TrackingNumber { get; set; } = string.Empty;

        public string Carrier { get; set; } = "Default Courier";

        public string ShippingStatus { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ShippedAt { get; set; }

        public DateTime? DeliveredAt { get; set; }
    }
}
