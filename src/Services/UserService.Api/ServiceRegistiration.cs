using Microsoft.EntityFrameworkCore;
using UserService.Api.Abstractions;
using UserService.Api.Context;

namespace UserService.Api
{
    public static class ServiceRegistiration
    {
        public static void AddUserPersistenceService(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<UserDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("MicrosoftSQL")));


            services.AddScoped<IUserService, Services.UserService>();
        }

    }
}
