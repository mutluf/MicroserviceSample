namespace AggregatorService.DTOs
{
    public class CourseDto
    {
        public string Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public string TeacherFullname { get; set; }
    }
}
