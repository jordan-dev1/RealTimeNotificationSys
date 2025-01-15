using StackExchange.Redis;
using System;

namespace RealTimeNotificationSys.Core.Services
{

   
    
        public class NotificationPublisher
        {
            private readonly ISubscriber _subscriber;

            // Constructor: Initializes the Redis connection and gets the Redis subscriber
            public NotificationPublisher()
            {
            // Connecting to the Redis server (using default localhost)
            var redis = ConnectionMultiplexer.Connect("localhost:6379,abortConnect=false");  // Make sure Redis is running on localhost
            _subscriber = redis.GetSubscriber();  // Getting the Redis subscriber
            }

            // Method to publish a notification message to Redis
            public void PublishNotification(string message)
            {
                try
                {
                    // Publish to the "notifications" channel
                    _subscriber.Publish("notifications", message);
                    Console.WriteLine("Notification published: " + message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error publishing notification: {ex.Message}");
                }
            }
        }
    

}