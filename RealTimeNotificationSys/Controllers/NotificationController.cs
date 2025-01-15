using Microsoft.AspNetCore.Mvc;
using RealTimeNotificationSys.Core.Interfaces;
using RealTimeNotificationSys.Core.Services;
using System.Security.Policy;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace RealTimeNotificationSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly NotificationPublisher _notificationPublisher;

        // Constructor: Inject NotificationPublisher and INotificationService
        public NotificationController(NotificationPublisher notificationPublisher, INotificationService notificationService)
        {
            _notificationPublisher = notificationPublisher;
            _notificationService = notificationService;
        }

        // POST api/notification/send (For Redis-published notifications)
        [HttpPost("send-redis")]
        public IActionResult SendRedisNotification([FromBody] string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return BadRequest("Message cannot be empty.");
            }

            // Use NotificationPublisher to publish the message to Redis
            _notificationPublisher.PublishNotification(message);

            return Ok("Notification sent to Redis successfully.");
        }

        // POST api/notification/send (For saving notification and sending via the service)
        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromQuery] int channelId, [FromQuery] string message)
        {
            try
            {
                // Save and send notification via the service (database or more advanced logic)
                var notification = await _notificationService.SaveNotificationAsync(channelId, message);
                await _notificationService.SendNotificationToChannelAsync(channelId, message);

                return Ok(new { notification.ID, notification.Message, notification.Timestamp });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET api/notification/{userId} (Retrieve notifications for a user)
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetNotifications(int userId)
        {
            try
            {
                var notifications = await _notificationService.GetNotificationsForUserAsync(userId);
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
