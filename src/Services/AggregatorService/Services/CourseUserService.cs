using AggregatorService.Abstractions;
using AggregatorService.Context;
using AggregatorService.DTOs;
using AggregatorService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AggregatorService.Services
{
    public class CourseUserService : ICourseUserService
    {
        private readonly CourseUserDbContext _context;
        private readonly HttpClient _httpClient;
        public CourseUserService(CourseUserDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }
        public DbSet<CourseUser> Table => _context.Set<CourseUser>();


        public async Task PostCourse(string courseId, string userId)
        {

            CourseUser courseUser = new()
            {
                CourseId = courseId,
                UserId = userId
            };

            await _context.AddAsync(courseUser);
            await _context.SaveChangesAsync();
        }


        public async Task<CourseDto> GetCourse(string courseId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:5005/api/courses/{courseId}");
            CourseDto course = await response.Content.ReadFromJsonAsync<CourseDto>();

            return course;
        }

   
        public async Task<List<UserDto>> GetUsers(string courseId)
        {
            var userIds = GetUserIds(courseId);
            var queryString = string.Join("&", userIds.Select(id => $"userId={id}"));

            var response = await _httpClient.GetAsync($"https://localhost:5005/api/users?{queryString}");

            var users = await response.Content.ReadFromJsonAsync<List<UserDto>>();

            return users;
        }


        private List<string> GetUserIds(string courseId)
        {
            var userIds = _context.CourseUsers.Where(c=> c.CourseId== courseId).Select(u=> u.UserId).ToList();
            return userIds;
        }
    }
}
