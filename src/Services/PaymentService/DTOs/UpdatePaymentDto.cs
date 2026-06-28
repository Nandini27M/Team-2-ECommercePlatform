using System.ComponentModel.DataAnnotations;

namespace PaymentService.DTOs
{
    public class UpdatePaymentDto
    {
        [Required]
        public string PaymentStatus { get; set; } = string.Empty;

        public string? TransactionId { get; set; }
    }
}