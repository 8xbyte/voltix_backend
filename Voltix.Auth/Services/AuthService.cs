using Voltix.Auth.Interfaces;
using Voltix.Auth.Models;

namespace Voltix.Auth.Services
{
    public interface IAuthService
    {
        public Task<string?> LoginAsync(string email, string password);
        public Task<string?> RegisterAsync(string name, string email, string password);
    }

    public class AuthService(IUserService userService, IJwtTokenService jwtTokenService) : IAuthService
    {
        private readonly IUserService _userService = userService;
        private readonly IJwtTokenService _jwtTokenService = jwtTokenService;

        public async Task<string?> LoginAsync(string email, string password)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
            {
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return null;
            }

            return _jwtTokenService.GenerateToken(new ITokenPayload()
            {
                UserId = user.Id
            });
        }

        public async Task<string?> RegisterAsync(string name, string email, string password)
        {
            var user = await _userService.GetUserByNameOrEmailAsync(name, email);
            if (user != null)
            {
                return null;
            }

            var newUser = await _userService.CreateUserAsync(new User()
            {
                Name = name,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            });

            return _jwtTokenService.GenerateToken(new ITokenPayload()
            {
                UserId = newUser.Id
            });
        }
    }   
}