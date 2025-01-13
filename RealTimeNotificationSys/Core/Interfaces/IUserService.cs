using Core.Entities;

namespace RealTimeNotificationSys.Core.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterUserAsync(string name, string email, string password);
    }

}
