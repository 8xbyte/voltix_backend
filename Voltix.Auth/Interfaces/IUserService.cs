using Voltix.Auth.Models;

namespace Voltix.Auth.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterAsync(string username, string email, string password);
        Task<string> AuthenticateAsync(string username, string password);
    }
}

