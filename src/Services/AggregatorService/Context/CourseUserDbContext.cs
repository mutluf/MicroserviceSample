using AggregatorService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AggregatorService.Context
{
    public class CourseUserDbContext: DbContext
    {
        public CourseUserDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CourseUser> CourseUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseUser>().
                HasKey(x => new
                {
                    x.CourseId,
                    x.UserId
                });
        }
    }
}
