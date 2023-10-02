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
        private readonly ISendEndpoint _bus;

        public CoursesContoller(ICourseUserService courseUserService)
        {
            _courseUserService = courseUserService;
            var bus = BusConfigurator.ConfigureBus();

            var sendToUri = new Uri($"{RabbitMqConstants.RabbitMqUri}/direct");
            _bus = bus.GetSendEndpoint(sendToUri).Result;
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

            await _bus.Send<IUserEnrolledCommand>(courseUser);

            return Ok();
        }
    }
}
