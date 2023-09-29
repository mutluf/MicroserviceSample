namespace CourseService.Api.Context
{
    public class CourseStoreDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string CoursesCollectionName { get; set; } = null!;
    }
}
