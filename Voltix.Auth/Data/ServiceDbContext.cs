using Microsoft.EntityFrameworkCore;
using Voltix.Auth.Models;

namespace Voltix.Auth.Data
{
    public class ServiceDbContext : DbContext
    {
        public ServiceDbContext(DbContextOptions<ServiceDbContext> options) : base(options) { }
        
        public DbSet<User> Users { get; set; }
    }
}

