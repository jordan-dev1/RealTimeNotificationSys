using Core.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RealTimeNotificationSys.Core.Interfaces;
using RealTimeNotificationSys.Infrastructure.Hubs;

namespace RealTimeNotificationSys.Core.Services
{

    public class NotificationService : INotificationService
    {
        private readonly IApplicationDbContext _context;
        private readonly IHubContext<NotificationHub, INotificationHub> _hubContext;

        public NotificationService(IApplicationDbContext context, IHubContext<NotificationHub, INotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task SendNotificationToChannelAsync(int channelId, string message)
        {
            var users = await _context.UserChannels
                .Where(uc => uc.ChannelId == channelId)
                .Select(uc => uc.User)
                .ToListAsync();

            foreach (var user in users)
            {
                await _hubContext.Clients.User(user.ID.ToString()).NotifyUser(user.ID.ToString(), message);
            }
        }

        public async Task<Notification> SaveNotificationAsync(int channelId, string message)
        {
            var notification = new Notification
            {
                ChannelId = channelId,
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return notification;
        }

        public async Task<IEnumerable<Notification>> GetNotificationsForUserAsync(int userId)
        {
            var channelIds = await _context.UserChannels
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.ChannelId)
                .ToListAsync();

            return await _context.Notifications
                .Where(n => channelIds.Contains(n.ChannelId))
                .OrderByDescending(n => n.Timestamp)
                .ToListAsync();
        }
    }


}
