using Microsoft.EntityFrameworkCore;
using Voltix.Auth.Models;
using Voltix.Auth.Data;

namespace Voltix.Auth.Services
{
    public interface IUserService
    {
        public Task<User?> GetUserByEmailAsync(string email);
        public Task<User?> GetUserByNameOrEmailAsync(string name, string email);
        public Task<User> CreateUserAsync(User user);
    }
    
    public class UserService(ServiceDbContext dbContext, IJwtTokenService jwtTokenService) : IUserService
    {
        private readonly ServiceDbContext _dbContext = dbContext;
        private readonly IJwtTokenService _jwtTokenService = jwtTokenService;

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(model => model.Id == id);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(model => model.Email == email);
        }

        public async Task<User?> GetUserByNameAsync(string name)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(model => model.Name == name);
        }

        public async Task<User?> GetUserByNameOrEmailAsync(string name, string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(model => model.Email == email || model.Name == name);
        }
        
        public async Task<User> CreateUserAsync(User user)
        {
            var userModel = await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return userModel.Entity;
        }
    }
}