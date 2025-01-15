using Core.Entities;
using RealTimeNotificationSys.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealTimeNotificationSys.Core.Interfaces
{
    public interface IChannelService
    {
        Task SubscribeUserToChannelAsync(int userId, List<int> channelIds);
    }
}
