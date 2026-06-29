using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationService.DTOs;
using NotificationService.Interfaces;

namespace NotificationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notifications = await _notificationService.GetAllAsync();
            return Ok(notifications);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            var notification = await _notificationService.GetByIdAsync(id);
            if (notification == null)
            {
                return NotFound(new
                {
                    Message = $"Notification with ID {id} not found."
                });
            }
            return Ok(notification);
        }






        [HttpPost("send")]
        public async Task<IActionResult> Send(NotificationRequestDto request)
        {
            await _notificationService.SendNotificationAsync(request);
            return Ok(new
            {
                Message = "Notification sent successfully."
            });
        }
    }
}
