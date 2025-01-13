using Microsoft.EntityFrameworkCore;
using Core.Entities;
using RealTimeNotificationSys.Core.Entities;
using RealTimeNotificationSys.Core.Interfaces;
using Microsoft.AspNetCore.Identity;



namespace RealTimeNotificationSys.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<UserChannel> UserChannels { get; set; }  // Added UserChannel for many-to-many relationship

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define many-to-many relationship for User and Channel
            modelBuilder.Entity<UserChannel>()
                .HasKey(uc => new { uc.UserId, uc.ChannelId });

            modelBuilder.Entity<UserChannel>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.SubscribedChannels)
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserChannel>()
                .HasOne(uc => uc.Channel)
                .WithMany(c => c.SubscribedUsers)
                .HasForeignKey(uc => uc.ChannelId);

            // Seed Channels
            modelBuilder.Entity<Channel>().HasData(
                new Channel { ID = 1, Name = "Sports" },
                new Channel { ID = 2, Name = "News" },
                new Channel { ID = 3, Name = "Tech" }
            );

            // Password hashing - you can use a password manager here, if desired.
            var passwordHasher = new PasswordHasher<User>();

            // Seed Users with hashed passwords
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    ID = 1,
                    Name = "John Doe",
                    Email = "john@example.com",
                    PasswordHash = passwordHasher.HashPassword(null, "johnpassword123")
                },
                new User
                {
                    ID = 2,
                    Name = "Jane Smith",
                    Email = "jane@example.com",
                    PasswordHash = passwordHasher.HashPassword(null, "janepassword123")
                }
            );

            // Seed User-Channel Relationship
            modelBuilder.Entity<UserChannel>().HasData(
                new UserChannel { UserId = 1, ChannelId = 1 }, // John subscribed to Sports
                new UserChannel { UserId = 1, ChannelId = 2 }, // John subscribed to News
                new UserChannel { UserId = 2, ChannelId = 3 }  // Jane subscribed to Tech
            );
        }
    }
}
