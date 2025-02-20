﻿using Core.Entities;

namespace RealTimeNotificationSys.Core.Entities
{
    public class UserChannel
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int ChannelId { get; set; }
        public Channel Channel { get; set; }
    }
}
