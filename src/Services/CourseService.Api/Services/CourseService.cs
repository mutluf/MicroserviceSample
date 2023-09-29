using CourseService.Api.Abstractions;
using CourseService.Api.Context;
using CourseService.Api.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CourseService.Api.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> _coursesCollection;

        public CourseService(
            IOptions<CourseStoreDatabaseSettings> courseStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                courseStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                courseStoreDatabaseSettings.Value.DatabaseName);

            _coursesCollection = mongoDatabase.GetCollection<Course>(
                courseStoreDatabaseSettings.Value.CoursesCollectionName);
        }

        public async Task<List<Course>> GetAsync() =>
            await _coursesCollection.Find(_ => true).ToListAsync();

        public async Task<Course?> GetAsync(string id) =>
            await _coursesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Course course) =>
            await _coursesCollection.InsertOneAsync(course);

        public async Task UpdateAsync(string id, Course course) =>
            await _coursesCollection.ReplaceOneAsync(x => x.Id == id, course);

        public async Task RemoveAsync(string id) =>
            await _coursesCollection.DeleteOneAsync(x => x.Id == id);

    }
}
