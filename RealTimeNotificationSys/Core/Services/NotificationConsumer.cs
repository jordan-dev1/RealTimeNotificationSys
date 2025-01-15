using RealTimeNotificationSys.Infrastructure.Hubs;
using StackExchange.Redis;
using Microsoft.AspNetCore.SignalR;

namespace RealTimeNotificationSys.Core.Services
{
    public class NotificationConsumer
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ISubscriber _subscriber;

        // Constructor: Injecting the SignalR Hub context and Redis subscriber
        public NotificationConsumer(IHubContext<NotificationHub> hubContext, IConnectionMultiplexer redisConnection)
        {
            _hubContext = hubContext;

            // Using the injected Redis connection to get the subscriber
            _subscriber = redisConnection.GetSubscriber();

            // Subscribe to Redis 'notifications' channel
            _subscriber.Subscribe("notifications", async (channel, message) =>
            {
                await SendNotificationToClients(message);  // Forward messages to SignalR clients
            });
        }

        // Method to forward received notification message to all SignalR clients
        private async Task SendNotificationToClients(string message)
        {
            try
            {
                await _hubContext.Clients.All.SendAsync("ReceiveNotification", message);  // Push to all connected clients via SignalR
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending notification to clients: {ex.Message}");
            }
        }
    }
}
