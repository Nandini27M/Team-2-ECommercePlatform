using System.ComponentModel.DataAnnotations;

namespace NotificationService.DTOs
{
    public class NotificationRequestDto
    {
        [Required(ErrorMessage = "Recipient is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Recipient { get; set; } = string.Empty;

        [Required(ErrorMessage = "Subject is required")]
        [MinLength(5, ErrorMessage = "Subject must be at least 5 characters")]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Message is required")]
        [MinLength(10, ErrorMessage = "Message must be at least 10 characters")]
        public string Message { get; set; } = string.Empty;

        [Required(ErrorMessage = "Notification type is required")]
        public string NotificationType { get; set; } = "Email";
    }
}
