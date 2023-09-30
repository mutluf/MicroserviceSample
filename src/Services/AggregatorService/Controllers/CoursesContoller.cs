using AggregatorService.Abstractions;
using AggregatorService.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AggregatorService.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CoursesContoller : ControllerBase
    {
        private readonly ICourseUserService _courseUserService;


        public CoursesContoller(ICourseUserService courseUserService)
        {
            _courseUserService = courseUserService;
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
        public IActionResult CoursesPost([FromBody] CourseUser courseUser)
        {
            _courseUserService.PostCourse(courseUser.CourseId, courseUser.UserId);

            return Ok();
        }
    }
}
