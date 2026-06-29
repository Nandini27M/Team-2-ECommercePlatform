using NotificationService.DTOs;
using NotificationService.Models;

namespace NotificationService.Interfaces;

public interface INotificationService
{
    Task<IEnumerable<Notification>> GetAllAsync();
    Task<Notification?> GetByIdAsync(int id);
    Task SendNotificationAsync(NotificationRequestDto request);
}