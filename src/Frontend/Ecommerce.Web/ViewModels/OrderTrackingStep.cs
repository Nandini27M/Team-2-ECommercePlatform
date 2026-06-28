namespace Ecommerce.Web.ViewModels
{
    public class OrderTrackingStep
    {
        public string Label { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
