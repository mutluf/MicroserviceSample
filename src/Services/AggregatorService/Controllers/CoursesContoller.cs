using Microsoft.AspNetCore.Mvc;

namespace AggregatorService.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CoursesContoller : ControllerBase
    {
        [HttpGet("{id}/users")]
        public string CoursesWithUsers([FromRoute] string id)
        {
            return "courses-users " + id;
        }

        [HttpPost]
        [Route("/courses/users")]
        public string CoursesPost()
        {
            return "post";
        }
    }
}
