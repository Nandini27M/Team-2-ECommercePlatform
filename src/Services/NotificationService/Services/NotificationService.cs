using NotificationService.DTOs;
using NotificationService.Interfaces;
using NotificationService.Models;

namespace NotificationService.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _repository;
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(
        INotificationRepository repository,
        ILogger<NotificationService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<Notification>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Notification?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task SendNotificationAsync(NotificationRequestDto request)
    {
        _logger.LogInformation("Sending notification to {Recipient}", request.Recipient);

        var notification = new Notification
        {
            Recipient = request.Recipient,
            Subject = request.Subject,
            Message = request.Message,
            NotificationType = request.NotificationType,
            Status = "Sent",
            CreatedAt = DateTime.Now
        };

        await _repository.AddAsync(notification);
        await _repository.SaveChangesAsync();

        _logger.LogInformation("Notification sent successfully to {Recipient}", request.Recipient);
    }
}