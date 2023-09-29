using Microsoft.EntityFrameworkCore;
using UserService.Api.Entities;

namespace UserService.Api.Context
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
