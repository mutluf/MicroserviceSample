using System.Linq.Expressions;
using UserService.Api.Entities;

namespace UserService.Api.Abstractions
{
    public interface IUserService
    {
        IQueryable<User> GetAll();
        IQueryable<User> GetWhere(Expression<Func<User, bool>> method);
        Task<User> GetByIdAysnc(string id);
        Task<User> GetSingleAysnc(Expression<Func<User, bool>> method);
        Task<bool> AddAysnc(User Model);
        bool Update(User Model);
        Task<int> SaveAysnc();
        void Delete(User Model);
    }
}
