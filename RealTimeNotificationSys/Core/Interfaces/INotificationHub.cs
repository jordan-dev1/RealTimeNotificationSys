namespace RealTimeNotificationSys.Core.Interfaces
{
    public interface INotificationHub
    {
        Task NotifyUser(string userId, string message);
        Task AddUserToGroup(string groupName, string connectionId);
        Task RemoveUserFromGroup(string groupName, string connectionId);
    }
}
