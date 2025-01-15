using Microsoft.AspNetCore.SignalR;
using RealTimeNotificationSys.Core.Interfaces;


namespace RealTimeNotificationSys.Infrastructure.Hubs
{
    public class NotificationHub : Hub<INotificationHub>
    {
        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            await Groups.AddToGroupAsync(userId, Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.UserIdentifier;
            await Groups.RemoveFromGroupAsync(userId, Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }

}
