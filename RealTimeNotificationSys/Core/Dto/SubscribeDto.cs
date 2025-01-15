namespace RealTimeNotificationSys.Core.Dto
{
    public class SubscribeDto
    {
        public int UserId { get; set; }
        public List<int> ChannelIds { get; set; }
    }
}
