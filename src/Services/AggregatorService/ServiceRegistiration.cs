using AggregatorService.Abstractions;
using AggregatorService.Context;
using Microsoft.EntityFrameworkCore;

namespace AggregatorService
{
    public static class ServiceRegistiration
    {
        public static void AddAggregatorService(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<CourseUserDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("MicrosoftSQL")));


            services.AddScoped<ICourseUserService, Services.CourseUserService>();
        }
    }
}
