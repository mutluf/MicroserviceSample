using CourseService.Api.Entities;
using System.Linq.Expressions;

namespace CourseService.Api.Abstractions
{
    public interface ICourseService
    {
        public Task<List<Course>> GetAsync();

        public Task<Course?> GetAsync(string id);

        public Task CreateAsync(Course course);

        public Task UpdateAsync(string id, Course updatedCourse);

        public Task RemoveAsync(string id);
    }
}
