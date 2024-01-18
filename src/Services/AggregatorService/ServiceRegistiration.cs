using AggregatorService.Abstractions;
using AggregatorService.Consumers;
using AggregatorService.Context;
using AggregatorService.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;


namespace AggregatorService
{
    public static class ServiceRegistiration
    {
        public static void AddAggregatorService(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<CourseUserDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("MicrosoftSQL")));

            services.AddScoped<ICourseUserService, CourseUserService>();
         

            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.AddConsumer<UserEnrolledConsumer>();

                busConfigurator.SetKebabCaseEndpointNameFormatter();
                busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
                {
                    

                    busFactoryConfigurator.Host("amqp://localhost/", hostConfigurator =>
                    {
                        hostConfigurator.Username("guest");
                        hostConfigurator.Password("guest");
                    });

                    busFactoryConfigurator.ReceiveEndpoint("test1", e =>
                    {
                        //e.Consumer<UserEnrolledConsumer>(context);
                        
                    });

                });
            });          

        }
    }
}
