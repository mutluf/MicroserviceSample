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
        private readonly IPublishEndpoint _publishEndpoint;

        public CoursesContoller(ICourseUserService courseUserService, IPublishEndpoint publishEndpoint)
        {
            _courseUserService = courseUserService;
            _publishEndpoint = publishEndpoint;
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
            //_courseUserService.PostCourse(courseUser.CourseId, courseUser.UserId);
            var data = new UserEnrolledEvent
            {

                UserId = courseUser.UserId,
                CourseId = courseUser.CourseId,
            };

            await _publishEndpoint.Publish(data);

            return Ok();
        }
    }
}
