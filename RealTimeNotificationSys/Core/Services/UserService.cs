using RealTimeNotificationSys.Core.Interfaces;
using RealTimeNotificationSys.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace RealTimeNotificationSys.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService(IApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<User> RegisterUserAsync(string name, string email, string password)
        {
            // Check if email already exists
            if (await _context.Users.AnyAsync(u => u.Email == email))
            {
                throw new InvalidOperationException("A user with this email already exists.");
            }

            // Create new user
            var user = new User
            {
                Name = name,
                Email = email
            };

            // Hash the password and set it to the user
            user.PasswordHash = _passwordHasher.HashPassword(user, password);

            // Save user to the database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }

}