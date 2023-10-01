using AggregatorService.Abstractions;
using AggregatorService.Entities;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace AggregatorService.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CoursesContoller : ControllerBase
    {
        private readonly ICourseUserService _courseUserService;
        private readonly IBus _bus;

        public CoursesContoller(ICourseUserService courseUserService, IBus bus)
        {
            _courseUserService = courseUserService;
            _bus = bus;
        }

        [HttpGet("{id}/users")]
        public async Task<IActionResult> CoursesWithUsers([FromRoute] string id)
        {
            var course = await _courseUserService.GetCourse(id);
            var users =  await _courseUserService.GetUsers(id);


            return Ok(new {users = users, course= course});
        }

        [HttpPost]
        [Route("/courses/users")]
        public async Task<IActionResult> CoursesPost([FromBody] CourseUser courseUser)
        {
            _courseUserService.PostCourse(courseUser.CourseId, courseUser.UserId);

            

            Uri endpointUri = new Uri("rabbitmq://localhost/demand"); // Update with your RabbitMQ URI

            var sendEndpoint = await _bus.GetSendEndpoint(endpointUri);
            await sendEndpoint.Send(courseUser);


            return Ok();
        }
    }
}
