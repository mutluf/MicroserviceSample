using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using UserService.Api.Abstractions;
using UserService.Api.Context;
using UserService.Api.Entities;

namespace UserService.Api.Services
{
    public class UserService : IUserService
    {
        private readonly UserDbContext _context;
        public UserService(UserDbContext context)
        {
            _context = context;
        }
        public DbSet<User> Table => _context.Set<User>();


        public async Task<List<User>> GetUserByIdList(List<string> userIds)
        {
            List<User> users = await Table.AsQueryable().Where(e => userIds.Contains(e.Id)).ToListAsync();
            return users;
        }
        
        public async Task<bool> AddAysnc(User Model)
        {
            EntityEntry entityEntry = await Table.AddAsync(Model);
            return entityEntry.State == EntityState.Added;
        }

        public void Delete(User Model)
        {
            Table.Remove(Model);
        }

        public async Task<int> SaveAysnc()
        {
            return await _context.SaveChangesAsync();
        }

        public bool Update(User Model)
        {
            EntityEntry entityEntry = _context.Update(Model);
            return entityEntry.State == EntityState.Modified;
        }

        public IQueryable<User> GetAll()
        {
            var query = Table.AsQueryable().AsNoTracking();
            return query;
        }

        public async Task<User?> GetByIdAysnc(string id)
        {
            var query = Table.AsQueryable().AsNoTracking();
            return await query.FirstOrDefaultAsync(data => data.Id ==id);
        }

        public async Task<User?> GetSingleAysnc(Expression<Func<User, bool>> method)
        {
            var query = await Table.AsQueryable().AsNoTracking().FirstOrDefaultAsync(method);
            return query;
        }

        public IQueryable<User> GetWhere(Expression<Func<User, bool>> method)
        {
            var query = Table.Where(method).AsQueryable().AsNoTracking();
            return query;
        }
    }
}
