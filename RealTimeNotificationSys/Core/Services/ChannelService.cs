using RealTimeNotificationSys.Core.Entities;
using RealTimeNotificationSys.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using RealTimeNotificationSys.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RealTimeNotificationSys.Core.Services
{
    public class ChannelService : IChannelService
    {
        private readonly IApplicationDbContext _context;

        public ChannelService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SubscribeUserToChannelAsync(int userId, List<int> channelIds)
        {
            // Validate if user exists
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                throw new InvalidOperationException("User not found.");

            // Validate and filter valid channels
            var channels = await _context.Channels
                .Where(c => channelIds.Contains(c.ID))
                .ToListAsync();
            if (channels.Count != channelIds.Count)
                throw new InvalidOperationException("One or more channels not found.");

            // Avoid duplicate subscriptions
            foreach (var channel in channels)
            {
                if (!_context.UserChannels.Any(uc => uc.UserId == userId && uc.ChannelId == channel.ID))
                {
                    _context.UserChannels.Add(new UserChannel
                    {
                        UserId = userId,
                        ChannelId = channel.ID
                    });
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
