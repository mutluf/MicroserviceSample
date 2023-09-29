using CourseService.Api.Abstractions;
using CourseService.Api.Context;

namespace CourseService.Api
{
    public static class ServiceRegistiration
    {
        public static void AddCoursePersistenceService(this IServiceCollection services, IConfiguration config)
        {

            services.Configure<CourseStoreDatabaseSettings>(config.GetSection("CourseStoreDatabase"));

            services.AddScoped<ICourseService, Services.CourseService>();
        }
    }
}
