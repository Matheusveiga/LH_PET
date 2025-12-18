using BCrypt.Net;
using LH_PET.Context;
using LH_PET.Models;
using Microsoft.EntityFrameworkCore;

namespace LH_PET.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task AddUserAsync(User user, string password)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            user.PasswordHash = hash;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password ?? string.Empty, passwordHash ?? string.Empty);
        }
    }
}
