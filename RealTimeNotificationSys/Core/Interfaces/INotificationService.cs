


using Core.Entities;
using System.Threading.Tasks;

namespace RealTimeNotificationSys.Core.Interfaces
{
    public interface INotificationService
    {
        Task SendNotificationToChannelAsync(int channelId, string message);
        Task<Notification> SaveNotificationAsync(int channelId, string message);
        Task<IEnumerable<Notification>> GetNotificationsForUserAsync(int userId);
    }

}
