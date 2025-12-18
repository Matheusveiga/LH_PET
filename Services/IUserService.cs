using LH_PET.Models;

namespace LH_PET.Services
{
    public interface IUserService
    {
        Task<User?> GetByUsernameAsync(string username);
        Task AddUserAsync(User user, string password);
        bool VerifyPassword(string password, string passwordHash);
    }
}
