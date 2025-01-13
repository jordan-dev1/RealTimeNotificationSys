using Core.Entities;
using RealTimeNotificationSys.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        // Property for hashed password
        [Required]
        public string PasswordHash { get; set; }

        public ICollection<UserChannel> SubscribedChannels { get; set; } = new List<UserChannel>();
    }
}
