using CourseService.Api.Abstractions;
using CourseService.Api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CourseService.Api.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<List<Course>> Get() =>
            await _courseService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Course>> Get(string id)
        {
            var course = await _courseService.GetAsync(id);

            if (course is null)
            {
                return NotFound();
            }

            return course;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Course course)
        {
            await _courseService.CreateAsync(course);

            return CreatedAtAction(nameof(Get), new { id = course.Id }, course);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Course course)
        {
            var course2 = await _courseService.GetAsync(id);

            if (course2 is null)
            {
                return NotFound();
            }

            course.Id = course.Id;

            await _courseService.UpdateAsync(id, course);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var course = await _courseService.GetAsync(id);

            if (course is null)
            {
                return NotFound();
            }

            await _courseService.RemoveAsync(id);

            return NoContent();
        }
    }
}
