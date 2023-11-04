using AggregatorService.Abstractions;
using MassTransit;
using Newtonsoft.Json;

namespace AggregatorService.Consumers
{
    public class UserEnrolledConsumer : IConsumer<UserEnrolledEvent>
    {
        private readonly ICourseUserService _courseUserService;

        public UserEnrolledConsumer(ICourseUserService courseUserService)
        {
            _courseUserService = courseUserService;
        }

      

        public async Task Consume(ConsumeContext<UserEnrolledEvent> context)
        {

            UserEnrolledEvent eventData = context.Message;

            await _courseUserService.AddCourseAsync(eventData.CourseId, eventData.UserId);
            await _courseUserService.SaveChangesAsync();

            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            Console.WriteLine($"ProductCreatedEvent message: {jsonMessage}");

            //paymet service e gönderirsin
            string courseId = eventData.CourseId;
            string userId = eventData.UserId;            
        }
    }
}
