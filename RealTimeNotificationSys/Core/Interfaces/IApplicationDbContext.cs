using Core.Entities;
using Microsoft.EntityFrameworkCore;
using RealTimeNotificationSys.Core.Entities;

namespace RealTimeNotificationSys.Core.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Notification> Notifications { get; set; }
        DbSet<Channel> Channels { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
