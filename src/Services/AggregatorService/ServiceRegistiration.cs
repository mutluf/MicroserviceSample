using AggregatorService.Abstractions;
using AggregatorService.Context;
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

            services.AddScoped<ICourseUserService, Services.CourseUserService>();


            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
               

                // Kuyruk oluşturma
                cfg.ReceiveEndpoint("my_queue", e =>
                {
                    // Kuyruk işlemleri burada tanımlanır.
                    e.PrefetchCount = 16; // İsteğe bağlı: Mesaj önceden yükleme sayısını ayarlayabilirsiniz.
                    e.UseConcurrencyLimit(8); // İsteğe bağlı: Paralel işleme sınırlarını ayarlayabilirsiniz.

                    // Kuyruğa gelen mesajları işleyecek tüketici (consumer) burada eklenir.
                    //e.Consumer<MyConsumer>();
                });
            });

            busControl.Start();


            busControl.Stop();
        }

    }
    }
