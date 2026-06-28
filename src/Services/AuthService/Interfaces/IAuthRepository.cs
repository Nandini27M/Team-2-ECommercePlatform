using AuthService.Entities;

namespace AuthService.Interfaces;

public interface IAuthRepository
{
    Task<User?> GetUserByEmailAsync(string email);

    Task AddUserAsync(User user);

    Task SaveChangesAsync();
}