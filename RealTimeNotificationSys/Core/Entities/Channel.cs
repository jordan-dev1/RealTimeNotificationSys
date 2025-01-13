using Core.Entities;
using RealTimeNotificationSys.Core.Entities;

namespace Core.Entities
{
    public class Channel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<UserChannel> SubscribedUsers { get; set; } = new List<UserChannel>();

    }
}
